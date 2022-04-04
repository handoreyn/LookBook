using Bakery.SharedKernel.Entities;

namespace Bakery.Employee.Core.Entities;

public class EmploymentLevel : BaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
}