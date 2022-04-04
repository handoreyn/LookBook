using System.ComponentModel.DataAnnotations;
using Bakery.SharedKernel.Dtos;
using Bakery.SharedKernel.Enums;

namespace Bakery.Employee.Core.Dtos;

public class CreateEmployeeDto : DtoBase
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public GenderType? Gender { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string EmployeeType { get; set; }
    [Required]
    public string EmployeeLevel { get; set; }
}