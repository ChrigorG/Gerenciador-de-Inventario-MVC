using Data.Context;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repositories
{
    public class StockMovementsRepository : BaseRepository<StockMovements>, IBaseRepository<StockMovements>
    {
        public StockMovementsRepository(AppDbContext db) : base(db) { }
    }
}
