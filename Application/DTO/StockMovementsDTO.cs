
using Domain.Entities;

namespace Application.DTO
{
    public class StockMovementsDTO : BaseDTO
    {
        public int IdProduct { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string UnitType { get; set; } = string.Empty;
        public string MovementType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public Product Product { get; set; } = new Product();
        public List<StockMovementsDTO> ListStockMovements { get; set; } = new List<StockMovementsDTO>();

        public void ValidatedDTO() => Validate(this);
    }
}
