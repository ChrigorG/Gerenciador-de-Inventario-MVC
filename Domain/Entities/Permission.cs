
namespace Domain.Entities
{
    public class Permission : BaseEntities
    {
        public int IdPermissionGroup { get; set; }
        public string Event { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
    }
}
