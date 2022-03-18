using System.ComponentModel.DataAnnotations;

namespace Bakery.Member.Core.Dtos.Member;

public class MemberRegisterCreateDto : DtoBase
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Email { get; set; }
}