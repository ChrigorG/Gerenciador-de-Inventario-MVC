using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Shared.Helper;

namespace Data.Repositories
{
    public class StockMovementsRepository : BaseRepository<StockMovements>, IStockMovementsRepository
    {
        public StockMovementsRepository(AppDbContext db, IHttpContextAccessor httpContextAccessor) : base(db, httpContextAccessor) { }

        public int SumQuantityProductInStock(int idProduct)
        {
            return _db.stocks.Where(x => x.IdProduct == idProduct && x.MovementType == Constants.Input).Sum(x => x.Quantity) - 
                   _db.stocks.Where(x => x.IdProduct == idProduct && x.MovementType == Constants.Output).Sum(x => x.Quantity);
        }
    }
}
