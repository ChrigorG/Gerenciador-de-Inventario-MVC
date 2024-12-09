using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Data.Repositories
{
    public class StockMovementsRepository : BaseRepository<StockMovements>, IStockMovementsRepository
    {
        public StockMovementsRepository(AppDbContext db, IHttpContextAccessor httpContextAccessor) : base(db, httpContextAccessor) { }
    }
}
