
namespace Domain.Entities
{
    public class StockMovements : BaseDomain
    {
        public int IdProduct { get; set; }
        public string MovementType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
