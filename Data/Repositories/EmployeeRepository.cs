using Data.Context;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IBaseRepository<Employee>
    {
        public EmployeeRepository(AppDbContext db) : base(db) { }
    }
}
