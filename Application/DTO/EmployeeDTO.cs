using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class EmployeeDTO : BaseDTO
    {
        [Required(ErrorMessage = "Campo grupo de permissão é obrigatório.")]
        public int IdPermissionGroup { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 a 150 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(30)]
        public string Function { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo email é obrigatório.")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(70, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 a 70 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo status é obrigatório.")]
        public bool Status { get; set; } = true;
        public PermissionGroupDTO permissionGroupDTO { get; set; } = null!;
        public List<EmployeeDTO> ListEmployees { get; set; } = null!;

        public void ValidatedDTO() => Validate(this);
    }
}
