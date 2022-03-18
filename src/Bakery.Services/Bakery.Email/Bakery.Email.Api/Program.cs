using Bakery.Email.Api.ServiceExtensions;
using Bakery.Email.Core.EventHandlers;
using Bakery.Email.Core.Events;
using Bakery.EventBus.Abstractions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEventBus();
builder.Services.AddEventHandler();
builder.Services.AddControllers();
var app = builder.Build();
app.UseRouting();
app.MapDefaultControllerRoute();
ConfigureEvents(app);
app.Run();


void ConfigureEvents(IApplicationBuilder app)
{
    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
    eventBus.Subscribe<MemberSignedInIntegrationEvent,MemberSignedInIntegrationEventHandler>();
    eventBus.Subscribe<MemberRegisteredIntegrationEvent,MemberRegisteredIntegrationEventHandler>();
    eventBus.Subscribe<MemberForgotPasswordIntegrationEvent,MemberForgotPasswordIntegrationEventHandler>();
}