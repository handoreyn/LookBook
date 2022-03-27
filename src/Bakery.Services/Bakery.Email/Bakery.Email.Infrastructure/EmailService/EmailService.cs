using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Bakery.Email.Core.Dtos.EmailService;
using Bakery.Email.Core.Services;
using Microsoft.Extensions.Logging;

namespace Bakery.Email.Infrastructure.EmailService;

public class EmailService : IEmailService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<EmailService> _logger;

    public EmailService(HttpClient httpClient, ILogger<EmailService> logger = null)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.sendgrid.com/v3/mail/send");
        _httpClient.DefaultRequestHeaders.Add("Authorization",
            "Bearer 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");

        _logger = logger;
    }

    public async Task SendEmail(EmailSendDto email)
    {
        var requestMessage = new HttpRequestMessage();
        requestMessage.Content = new StringContent(JsonSerializer.Serialize(email),Encoding.UTF8,MediaTypeNames.Application.Json);
        requestMessage.Method = HttpMethod.Post;
        var responseMessage = await _httpClient.SendAsync(requestMessage);
        _logger.LogInformation("Email has been sent.");
        responseMessage.EnsureSuccessStatusCode();
        _logger.LogInformation("Email sent ensured to email successfully delivered.");
    }
}