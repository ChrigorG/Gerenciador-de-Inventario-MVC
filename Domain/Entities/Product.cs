
namespace Domain.Entities
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool Status { get; set; } = true; // Ativa e desativa um produto ao inves de deletar
    }
}
