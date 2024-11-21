
using Data.Context;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repositories
{
    public class PermissionRepository : BaseRepository<Permission>, IBaseRepository<Permission>
    {
        public PermissionRepository(AppDbContext db) : base(db) { }
    }
}
