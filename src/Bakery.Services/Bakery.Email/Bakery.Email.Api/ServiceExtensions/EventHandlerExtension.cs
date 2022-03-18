using Bakery.Email.Core.EventHandlers;
using Bakery.Email.Core.Events;

public static class EventHandlerExtension
{
    public static void AddEventHandler(this IServiceCollection services)
    {
        services.AddTransient<MemberForgotPasswordIntegrationEventHandler>();
        services.AddTransient<MemberSignedInIntegrationEventHandler>();
        services.AddTransient<MemberRegisteredIntegrationEventHandler>();
    }
}