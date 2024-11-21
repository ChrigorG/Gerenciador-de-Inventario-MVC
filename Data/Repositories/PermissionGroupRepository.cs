using Data.Context;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repositories
{
    public class PermissionGroupRepository : BaseRepository<PermissionGroup>, IBaseRepository<PermissionGroup>
    {
        public PermissionGroupRepository(AppDbContext db) : base(db) { }
    }
}
