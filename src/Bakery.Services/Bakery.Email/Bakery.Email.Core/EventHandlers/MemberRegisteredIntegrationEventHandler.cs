using Bakery.Email.Core.Events;
using Bakery.EventBus.Abstractions;
using Microsoft.Extensions.Logging;

namespace Bakery.Email.Core.EventHandlers;

public class MemberRegisteredIntegrationEventHandler: IIntegrationEventHandler<MemberRegisteredIntegrationEvent>
{
    private readonly ILogger<MemberRegisteredIntegrationEventHandler> _logger;

    public MemberRegisteredIntegrationEventHandler(ILogger<MemberRegisteredIntegrationEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MemberRegisteredIntegrationEvent @event)
    {
        _logger.LogInformation("MemberRegisteredIntegrationEvent has been received: {EventId}", @event.Id);
        _logger.LogTrace("Member Registered email sent to {Email}", @event.Email);
        
        return Task.CompletedTask;
    }
}