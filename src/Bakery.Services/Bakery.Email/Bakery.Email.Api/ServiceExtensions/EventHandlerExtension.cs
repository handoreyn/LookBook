using Bakery.Email.Core.EventHandlers;
using Bakery.Email.Core.Events;

public static class EventHandlerExtension
{
    public static void AddEventHandler(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventHandler<MemberForgotPasswordIntegrationEvent>, MemberForgotPasswordIntegrationEventHandler>();
        services.AddScoped<IIntegrationEventHandler<MemberSignedInIntegrationEvent>, MemberSignedInIntegrationEventHandler>();
        services.AddScoped<IIntegrationEventHandler<MemberRegisteredIntegrationEvent>, MemberRegisteredIntegrationEventHandler>();
    }
}