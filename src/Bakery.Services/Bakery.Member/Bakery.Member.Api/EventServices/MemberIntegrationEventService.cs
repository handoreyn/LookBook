using Bakery.EventBus.Abstractions;
using Bakery.EventBus.Events;

namespace Bakery.Member.Api.EventServices;

public class MemberIntegrationEventService : IMemberIntegrationEventService, IDisposable
{
    private readonly ILogger<MemberIntegrationEventService> _logger;
    private readonly IEventBus _eventBus;
    private volatile bool disposedValue;
    
    public MemberIntegrationEventService(ILogger<MemberIntegrationEventService> logger, IEventBus eventBus)
    {
        _logger = logger;
        _eventBus = eventBus;
    }

    protected virtual void Dispose(bool disposing)
    {
        if(disposedValue) return;
        disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Task PublishThroughEventBusAsync(IntegrationEvent @event)
    {
        try
        {
            _logger.LogInformation("---- Publishing integration event: {IntegrationEventId_published} from MemberApi",
                @event.Id);
            _eventBus.Publish(@event);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "ERROR Publishing integration event: {IntegrationEventId} from MemberApi - ({@IntegrationEvent})",
                @event.Id, @event);
        }

        return Task.CompletedTask;
    }
}