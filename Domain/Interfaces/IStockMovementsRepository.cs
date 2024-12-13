
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IStockMovementsRepository : IBaseRepository<StockMovements>
    {
        int SumQuantityProductInStock(int idProduct);
    }
}
