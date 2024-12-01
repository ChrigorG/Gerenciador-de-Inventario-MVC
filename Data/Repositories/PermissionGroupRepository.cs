using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PermissionGroupRepository : BaseRepository<PermissionGroup>, IPermissionGroupRepository
    {
        public PermissionGroupRepository(AppDbContext db) : base(db) { }
    }
}
