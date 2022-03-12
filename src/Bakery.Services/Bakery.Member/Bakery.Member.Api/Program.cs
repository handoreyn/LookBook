var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepository();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
