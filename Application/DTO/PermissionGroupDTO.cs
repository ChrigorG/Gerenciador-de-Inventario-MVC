using System.ComponentModel.DataAnnotations;
using Shared.Helper;

namespace Application.DTO
{
    public sealed class PermissionGroupDTO : BaseDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter 3 a 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        [StringLength(400)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "O status é obrigatório")]
        public bool Status { get; set; } = true;

        [Required(ErrorMessage = "A ação da tela do funcionário é obrigatório")]
        public string ActionEmployee { get; set; } = Constants.PermissionAccess;

        [Required(ErrorMessage = "A ação da tela do produto é obrigatório")]
        public string ActionProduct { get; set; } = Constants.PermissionAccess;

        [Required(ErrorMessage = "A ação da tela do estoque de momentação é obrigatório")]
        public string ActionStockMovements { get; set; } = Constants.PermissionAccess;

        [Required(ErrorMessage = "A ação da tela do grupo de permissão é obrigatório")]
        public string ActionPermissionGroup { get; set; } = Constants.PermissionDenied;
        public List<PermissionGroupDTO> ListPermissionGroup { get; set; } = new List<PermissionGroupDTO>();

        public void ValidatedDTO() => Validate(this);
    }
}
