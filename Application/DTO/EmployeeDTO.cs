using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class EmployeeDTO : BaseDTO
    {
        [Required]
        public int IdPermissionGroup { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(30)]
        public string Function { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(70)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public bool Status { get; set; } = true;
        public PermissionGroupDTO permissionGroupDTO { get; set; } = null!;
        public List<EmployeeDTO> ListEmployees { get; set; } = null!;

        public void ValidatedDTO() => Validate(this);
    }
}
