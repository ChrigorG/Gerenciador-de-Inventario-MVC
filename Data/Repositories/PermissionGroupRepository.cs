using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Data.Repositories
{
    public class PermissionGroupRepository : BaseRepository<PermissionGroup>, IPermissionGroupRepository
    {
        public PermissionGroupRepository(AppDbContext db, IHttpContextAccessor httpContextAccessor) : base(db, httpContextAccessor) { }
    }
}
