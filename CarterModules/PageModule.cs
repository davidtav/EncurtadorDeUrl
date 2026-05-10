using Carter;

namespace EncurtadorDeUrl.CarterModules;

public class PageModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (IWebHostEnvironment env) =>
        {
            var filePath = Path.Combine(env.WebRootPath, "index.html");

            var html = await System.IO.File.ReadAllTextAsync(filePath);

            return Results.Content(html, "text/html; charset=utf-8");
        });
    }
}