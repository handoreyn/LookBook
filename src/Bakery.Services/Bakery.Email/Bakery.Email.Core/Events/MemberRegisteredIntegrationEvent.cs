namespace Bakery.Email.Core.Events;

public class MemberRegisteredIntegrationEvent : IntegrationEvent
{
    public string MemberId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }

    public MemberRegisteredIntegrationEvent(string memberId, string username, string email)
    {
        MemberId = memberId;
        Username = username;
        Email = email;
    }
}