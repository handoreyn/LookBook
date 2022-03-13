public class MemberSignedIntegrationEvent : IntegrationEvent
{
    public string MemberId { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string ClientName { get; set; }
    public string Location { get; set; }

    public MemberSignedIntegrationEvent(string memberId, string name, string username, string clientName, string location)
    {
        MemberId = memberId;
        Name = name;
        Username = username;
        ClientName = clientName;
        Location = location;
    }
}