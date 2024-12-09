using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public sealed class LoginDTO
    {
        [StringLength(100)]
        [Required(ErrorMessage = "Informe um email")]
        public string Email { get; set; } = string.Empty;

        [StringLength(70)]
        [Required(ErrorMessage = "Informe uma senha")]
        public string Password { get; set; } = string.Empty;
    }
}
