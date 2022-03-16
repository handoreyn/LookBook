namespace Bakery.Email.Core.Events;

public class MemberSignedInIntegrationEvent : IntegrationEvent
{
    public string MemberId { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string ClientName { get; set; }
    public string Location { get; set; }
    public string Email { get; set; }

    public MemberSignedInIntegrationEvent(string memberId, string name, string username, string clientName, string location, string email)
    {
        MemberId = memberId;
        Name = name;
        Username = username;
        ClientName = clientName;
        Location = location;
        Email = email;
    }
}