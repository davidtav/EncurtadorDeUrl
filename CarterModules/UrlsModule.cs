using Carter;

namespace EncurtadorDeUrl.CarterModules;

public class UrlsModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var urls = app.MapGroup("/urls");

        urls.MapPost("/", async (CreateShortUrlRequest request) =>
        {
            // var chunk = "abc123";
            //
            // var response = new CreateShortUrlResponse(
            //     OriginalUrl: request.Url,
            //     ShortUrl: $"http://localhost:5020/urls/{chunk}",
            //     Chunk: chunk
            // );
            //
            // return Results.Ok(response);
        });

        urls.MapGet("/{chunk}", async (string chunk) =>
        {
            //return Results.Redirect("https://www.google.com");
        });
    }
}

public record CreateShortUrlRequest(string Url);

public record CreateShortUrlResponse(
    string OriginalUrl,
    string ShortUrl,
    string Chunk
);