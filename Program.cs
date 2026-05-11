using Carter;
using LiteDB;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();
builder.Services.AddSingleton<ILiteDatabase, LiteDatabase>(x=> new LiteDatabase("short.db"));
var app = builder.Build();
app.UseStaticFiles();
app.MapCarter();
app.Run();