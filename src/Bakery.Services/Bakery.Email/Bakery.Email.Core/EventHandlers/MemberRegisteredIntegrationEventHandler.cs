using Bakery.Email.Core.Dtos.EmailService;
using Bakery.Email.Core.Events;
using Bakery.Email.Core.Services;
using Bakery.EventBus.Abstractions;
using Microsoft.Extensions.Logging;

namespace Bakery.Email.Core.EventHandlers;

public class MemberRegisteredIntegrationEventHandler : IIntegrationEventHandler<MemberRegisteredIntegrationEvent>
{
    private readonly ILogger<MemberRegisteredIntegrationEventHandler> _logger;
    private readonly IEmailService _emailService;

    public MemberRegisteredIntegrationEventHandler(ILogger<MemberRegisteredIntegrationEventHandler> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public Task Handle(MemberRegisteredIntegrationEvent @event)
    {
        _logger.LogInformation("MemberRegisteredIntegrationEvent has been received: {EventId}", @event.Id);
        var email = new EmailSendDto
        {
            From = new From { Email = "me@fatih.software.com" },
            Personalizations = new List<Personalization>
            {
                new Personalization
                {
                        To=new List<From>{
                            new From{
                                Email=@event.Email
                            }
                        }
                }
            },
            Content = new List<Content>
            {
                new Content{
                    Type="html",
                    Value=@$"<h5>Hi, {@event.Email},</h5><p>This email has been sent to you by an event handler.</p>"
                }
            }
        };
        _emailService.SendEmail(email);
        _logger.LogTrace("Member Registered email sent to {Email}", @event.Email);

        return Task.CompletedTask;
    }
}