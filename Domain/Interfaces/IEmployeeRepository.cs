using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Employee? ValidatedUser(string email, string password);
    }
}
