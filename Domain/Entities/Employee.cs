
namespace Domain.Entities
{
    public class Employee : BaseEntities
    {
        public int IdPermissionGroup { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Function { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Status { get; set; } = true; // Ativa ou desativa um funcionario ao inves de deletar
    }
}
