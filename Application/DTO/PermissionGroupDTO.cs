
using Domain.Entities;

namespace Application.DTO
{
    public class PermissionGroupDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
        public List<PermissionGroupDTO> ListPermissionGroup { get; set; } = new List<PermissionGroupDTO>();
    }
}
