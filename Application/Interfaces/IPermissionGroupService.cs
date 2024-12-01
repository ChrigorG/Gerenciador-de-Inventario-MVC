using Application.DTO;

namespace Application.Interfaces
{
    public interface IPermissionGroupService
    {
        Task<PermissionGroupDTO> GetPermissionGroup();

        Task<PermissionGroupDTO> FormPermissionGroup(int id);

        Task<PermissionGroupDTO> SavePermissionGroup(PermissionGroupDTO permissionGroupDTO);

        Task<PermissionGroupDTO> DeletePermissionGroup(int id);
    }
}
