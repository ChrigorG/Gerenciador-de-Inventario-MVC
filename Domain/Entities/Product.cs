
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Product : BaseEntities
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(400)]
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório")]
        [Column("price", TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O status é obrigatório")]
        [Column("status")]
        public bool Status { get; set; } = true; // Ativa e desativa um produto ao inves de deletar

        [Required(ErrorMessage = "O tipo de unidade é obrigatório")]
        [Column("unit_type", TypeName = "char(2)")]
        public string UnitType { get; set; } = string.Empty;
    }
}
