using Bakery.EventBus;
using Bakery.EventBusRabbitMQ;

namespace Bakery.Email.Api.ServiceExtensions;

public static class EventBusExtension
{
    public static void AddEventBus(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IEventBusSubscriptionManager,InMemoryEventBusSubscriptionEventManager>()
            .AddSingleton<IRabbitMQPersistentConnection, DefaultRabbitMQPersistentConnection>()
            .AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>();
    }
}