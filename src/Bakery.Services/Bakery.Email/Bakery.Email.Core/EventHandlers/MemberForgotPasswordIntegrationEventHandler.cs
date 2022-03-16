using Bakery.Email.Core.Events;
using Microsoft.Extensions.Logging;

namespace Bakery.Email.Core.EventHandlers;

public class MemberForgotPasswordIntegrationEventHandler : IIntegrationEventHandler<MemberForgotPasswordIntegrationEvent>
{
    private ILogger<MemberForgotPasswordIntegrationEventHandler> _logger;

    public MemberForgotPasswordIntegrationEventHandler(ILogger<MemberForgotPasswordIntegrationEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MemberForgotPasswordIntegrationEvent @event)
    {
        _logger.LogTrace("Member forgot password event with id: {EventId} handled and mail sent to '{MemberEmail}'",
            @event.Id, @event.Email);

        return Task.CompletedTask;
    }
}