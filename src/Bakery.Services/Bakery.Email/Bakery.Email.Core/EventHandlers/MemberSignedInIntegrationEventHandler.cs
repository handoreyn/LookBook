using Bakery.Email.Core.Dtos.EmailService;
using Bakery.Email.Core.Events;
using Bakery.Email.Core.Services;
using Bakery.EventBus.Abstractions;
using Microsoft.Extensions.Logging;

namespace Bakery.Email.Core.EventHandlers;

public class MemberSignedInIntegrationEventHandler : IIntegrationEventHandler<MemberSignedInIntegrationEvent>
{
    private readonly ILogger<MemberSignedInIntegrationEventHandler> _logger;
    private readonly IEmailService _emailService;

    public MemberSignedInIntegrationEventHandler(ILogger<MemberSignedInIntegrationEventHandler> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public Task Handle(MemberSignedInIntegrationEvent @event)
    {
        _logger.LogInformation("MemberSignedInIntegrationEvent has been received.");

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
        _logger.LogInformation("Event handled: {EventId} - {Event}", @event.Id, @event);
        return Task.CompletedTask;
    }
}