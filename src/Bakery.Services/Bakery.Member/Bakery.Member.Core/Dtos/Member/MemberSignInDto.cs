namespace Bakery.Member.Core.Dtos.Member;

public class MemberSignInDto:DtoBase
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Location { get; set; }
    public string Client { get; set; }
}