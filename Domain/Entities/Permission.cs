
namespace Domain.Entities
{
    public class Permission : BaseDomain
    {
        public int IdPermissionGroup { get; set; }
        public string Event { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
    }
}
