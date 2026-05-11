using Carter;
using LiteDB;

namespace EncurtadorDeUrl.CarterModules;

public class UrlsModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var urls = app.MapGroup("/urls");

      
        urls.MapPost("/", (CreateShortUrlRequest request, ILiteDatabase db, HttpContext context) =>
        {
            
            if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
            {
                return Results.BadRequest(new { ErrorMessage = "Invalid Url" });
            }

            var collection = db.GetCollection<UrlMapping>("urls");
            
            var url = new UrlMapping
            {
                OriginalUrl = request.Url,
                Chunk = Guid.NewGuid().ToString("N")[..9] 
            };
            
            collection.Insert(url);

            
            var rawShortUrl = $"{context.Request.Scheme}://{context.Request.Host}/{url.Chunk}"; 

            var response = new CreateShortUrlResponse(
                url.OriginalUrl,
                rawShortUrl,
                url.Chunk
            );

            return Results.Ok(response);
        });

       
        urls.MapGet("/{chunk}", (string chunk, ILiteDatabase db) =>
        {
           var collection = db.GetCollection<UrlMapping>("urls");
           var mapping = collection.FindOne(x => x.Chunk == chunk);
           
           if (mapping is null) return Results.NotFound();

           return Results.Redirect(mapping.OriginalUrl);
        });
    }
}



public class UrlMapping {
    public int Id { get; set; }
    public string OriginalUrl { get; set; }
    public string Chunk { get; set; }
}

public record CreateShortUrlRequest(string Url);

public record CreateShortUrlResponse(
    string OriginalUrl,
    string ShortUrl,            
    string Chunk
);