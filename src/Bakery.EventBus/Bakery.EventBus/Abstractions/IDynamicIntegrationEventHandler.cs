public interface IDynamicIntegrationEventHandler
{
    Task Handle(dynamic eventData);
}