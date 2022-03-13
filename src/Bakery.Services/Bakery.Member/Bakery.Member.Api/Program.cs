using System.Text.Json.Serialization;
using Bakery.Member.Api.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepository();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddEventBus();
var app = builder.Build();

// app.UseExceptionHandler();
app.UseRouting();
app.MapDefaultControllerRoute();
app.Run();