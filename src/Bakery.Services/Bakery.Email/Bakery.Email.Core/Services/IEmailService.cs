using Bakery.Email.Core.Dtos.EmailService;

namespace Bakery.Email.Core.Services;

public interface IEmailService
{
    Task SendEmail(EmailSendDto email);
}