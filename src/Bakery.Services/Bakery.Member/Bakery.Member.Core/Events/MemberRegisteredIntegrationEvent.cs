public class MemberRegisteredIntegrationEvent : IntegrationEvent
{
    public string MemberId { get; set; }
    public string Username { get; set; }


    public MemberRegisteredIntegrationEvent(string memberId, string username)
    {
        MemberId = memberId;
        Username = username;
    }
}