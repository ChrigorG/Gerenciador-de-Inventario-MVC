
namespace Domain.Entities
{
    public class PermissionGroup : BaseDomain
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; 
        public bool Status { get; set; } = true;
    }
}
