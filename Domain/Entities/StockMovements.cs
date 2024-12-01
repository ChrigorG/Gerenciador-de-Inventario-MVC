
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class StockMovements : BaseEntities
    {
        [Required]
        [Column("movement_type")]
        public string MovementType { get; set; } = string.Empty;

        [Required(ErrorMessage = "A quantidade é obrigatório")]
        [Column("quantity")]
        public int Quantity { get; set; }

        [Required]
        [Column("movement_date")]
        public DateTime MovementDate { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column("id_product")]
        public int IdProduct { get; set; }

        [ForeignKey(nameof(IdProduct))]
        public Product Product { get; set; } = null!; // Propriedade de navegação (não nula)
    }
}
