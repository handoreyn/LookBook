namespace Bakery.Member.Api.EventServices;

public interface IMemberIntegrationEventService
{
    Task PublishThroughEventBusAsync(IntegrationEvent @event);
}