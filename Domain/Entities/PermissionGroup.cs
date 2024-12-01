using Shared.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PermissionGroup : BaseEntities
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        [StringLength(400)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "O status é obrigatório")]
        [Column("status")]
        public bool Status { get; set; } = true;

        [Required(ErrorMessage = "A ação da tela do funcionário é obrigatório")]
        [Column("action_employee")]
        [StringLength(6)]
        public string ActionEmployee { get; set; } = string.Empty;

        [Required(ErrorMessage = "A ação da tela do produto é obrigatório")]
        [Column("action_product")]
        [StringLength(6)]
        public string ActionProduct { get; set; } = string.Empty;

        [Required(ErrorMessage = "A ação da tela do estoque de momentação é obrigatório")]
        [Column("action_stock_movements")]
        [StringLength(6)]
        public string ActionStockMovements { get; set; } = string.Empty;

        [Required(ErrorMessage = "A ação da tela do grupo de permissão é obrigatório")]
        [Column("action_permission_group")]
        [StringLength(6)]
        public string ActionPermissionGroup { get; set; } = Constants.PermissionDenied;
    }
}
