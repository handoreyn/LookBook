using Bakery.EventBus;
using Bakery.EventBusRabbitMQ;

namespace Bakery.Member.Api.ServiceExtensions;

public static class EventBusExtension
{
    public static void AddEventBus(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IRabbitMQPersistentConnection, DefaultRabbitMQPersistentConnection>()
            .AddSingleton<IEventBusSubscriptionManager,InMemoryEventBusSubscriptionEventManager>()
            .AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMq>();
    }
}