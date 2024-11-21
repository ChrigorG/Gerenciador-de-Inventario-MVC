
namespace Domain.Entities
{
    public class PermissionGroup : BaseEntities
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; 
        public bool Status { get; set; } = true;
    }
}
