namespace Bakery.Member.Core.Dtos.Member;

public class MemberUpdateDto : DtoBase
{
    public DateTime? BirthDate { get; set; }
    public GenderType? Gender { get; set; }
    public string Country { get; set; }
    public string Username { get; set; }
}