var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepository();
builder.Services.AddControllers();
var app = builder.Build();

// app.UseExceptionHandler();
app.UseRouting();
app.MapDefaultControllerRoute();
app.Run();
