
namespace Application.DTO
{
    public class EmployeeDTO : BaseDTO
    {
        public int IdPermissionGroup { get; set; }
        public string NamePermissionGroup { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Function { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Status { get; set; } = true; // Ativa ou desativa um funcionario ao inves de deletar
        public List<EmployeeDTO> ListEmployees { get; set; } = new List<EmployeeDTO>();
    }
}
