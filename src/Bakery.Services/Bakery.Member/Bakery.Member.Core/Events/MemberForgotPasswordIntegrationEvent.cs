public class MemberForgotPasswordIntegrationEvent : IntegrationEvent
{
    public string MemberId { get; set; }
    public string Username { get; set; }
    public string VerificationCode { get; set; }

    public MemberForgotPasswordIntegrationEvent(string memberId, string username, string verificationCode)
    {
        MemberId = memberId;
        Username = username;
        VerificationCode = verificationCode;
    }
}