
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Employee : BaseEntities
    {
        [Required]
        [Column("id_permission_group")]
        public int IdPermissionGroup { get; set; }

        [Required]
        [Column("name")]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [Column("function")]
        [StringLength(30)]
        public string Function { get; set; } = string.Empty;

        [Required]
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("password")]
        [StringLength(70)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Column("status")]
        public bool Status { get; set; } = true; // Ativa ou desativa um funcionario ao inves de deletar
    }
}
