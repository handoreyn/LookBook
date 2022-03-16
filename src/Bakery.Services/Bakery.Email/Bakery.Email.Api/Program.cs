using Bakery.Email.Api.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddEventBus();
builder.Services.AddEventHandler();
builder.Services.AddControllers();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();