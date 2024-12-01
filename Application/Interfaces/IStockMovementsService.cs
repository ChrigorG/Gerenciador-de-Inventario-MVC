using Application.DTO;

namespace Application.Interfaces
{
    public interface IStockMovementsService
    {
        Task<StockMovementsDTO> GetStockMovements();
    }
}
