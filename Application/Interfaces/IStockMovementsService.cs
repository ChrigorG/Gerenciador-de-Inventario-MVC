using Application.DTO;

namespace Application.Interfaces
{
    public interface IStockMovementsService
    {
        StockMovementsDTO GetStockMovements();
        StockMovementsDTO FormProductInStock();
        StockMovementsDTO AddProductInStock(StockMovementsDTO stockMovementsDTO);
        StockMovementsDTO DetailProductInStock(int id);
    }
}
