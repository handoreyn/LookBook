using Bakery.SharedKernel.Dtos;

namespace Bakery.Member.Core.Dtos.Member;

public class MemberRegisterCreateResultDto:DtoBase
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string JwtToken { get; set; }
}