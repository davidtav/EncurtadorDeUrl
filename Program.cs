using Carter;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();
var app = builder.Build();
app.UseStaticFiles();
app.MapCarter();
app.Run();