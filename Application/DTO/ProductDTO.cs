using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public sealed class ProductDTO : BaseDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter 3 a 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        [StringLength(400)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O status é obrigatório")]
        public bool Status { get; set; } = true; // Ativa e desativa um produto ao inves de deletar

        [Required(ErrorMessage = "O tipo de unidade é obrigatório")]
        public string UnitType { get; set; } = string.Empty;

        public List<ProductDTO> ListProducts { get; set; } = new List<ProductDTO>();

        public void ValidatedDTO() => Validate(this);
    }
}
