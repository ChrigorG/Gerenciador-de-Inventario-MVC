
namespace Application.DTO
{
    public class ProductDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; } = true; // Ativa e desativa um produto ao inves de deletar
        public string UnitType { get; set; } = string.Empty;
        public List<ProductDTO> ListProducts { get; set; } = new List<ProductDTO>();
    }
}
