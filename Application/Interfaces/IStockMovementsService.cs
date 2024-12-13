using Application.DTO;

namespace Application.Interfaces
{
    public interface IStockMovementsService
    {
        StockMovementsDTO GetStockMovements();
        StockMovementsDTO FormProductInStock();
        StockMovementsDTO FormRemoveProductInStock();
        StockMovementsDTO AddProductInStock(StockMovementsDTO stockMovementsDTO);
        StockMovementsDTO RemoveProductInStock(StockMovementsDTO stockMovementsDTO);
        StockMovementsDTO DetailProductInStock(int id);
    }
}
