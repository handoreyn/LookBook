using System.ComponentModel.DataAnnotations;
using Bakery.SharedKernel.Dtos;

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