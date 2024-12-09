using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Data.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext db, IHttpContextAccessor httpContextAccessor) : base(db, httpContextAccessor) { }

        public Employee? ValidatedUser(string email, string password)
        {
            return _db.employees.FirstOrDefault(x => x.Email == email && x.Password == password);
        }
    }
}
