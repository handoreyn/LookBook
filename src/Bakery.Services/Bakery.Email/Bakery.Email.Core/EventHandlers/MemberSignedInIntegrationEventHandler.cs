using Bakery.Email.Core.Events;
using Bakery.EventBus.Abstractions;
using Microsoft.Extensions.Logging;

namespace Bakery.Email.Core.EventHandlers;

public class MemberSignedInIntegrationEventHandler : IIntegrationEventHandler<MemberSignedInIntegrationEvent>
{
    private readonly ILogger<MemberSignedInIntegrationEventHandler> _logger;

    public MemberSignedInIntegrationEventHandler(ILogger<MemberSignedInIntegrationEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MemberSignedInIntegrationEvent @event)
    {
        _logger.LogInformation("MemberSignedInIntegrationEvent has been received.");
        _logger.LogTrace("Member sign in notification has been send");
        
        return Task.CompletedTask;
    }
}