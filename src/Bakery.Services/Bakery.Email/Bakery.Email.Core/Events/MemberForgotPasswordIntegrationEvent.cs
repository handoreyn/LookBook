namespace Bakery.Email.Core.Events;

public class MemberForgotPasswordIntegrationEvent : IntegrationEvent
{
    public string MemberId { get; set; }
    public string Username { get; set; }
    public string VerificationCode { get; set; }
    public string Email { get; set; }

    public MemberForgotPasswordIntegrationEvent(string memberId, string username, string verificationCode, string email)
    {
        MemberId = memberId;
        Username = username;
        VerificationCode = verificationCode;
        Email = email;
    }
}