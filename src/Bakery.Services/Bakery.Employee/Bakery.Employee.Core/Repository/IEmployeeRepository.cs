using Bakery.Employee.Core.Dtos;
using Bakery.Employee.Core.Entities;
using Bakery.MongoDBRepository;

namespace Bakery.Employee.Core.Repository;

public interface IEmployeeRepository : IRepository<EmployeeEntity>
{
    Task CreateEmploye(CreateEmployeeDto employee);
}