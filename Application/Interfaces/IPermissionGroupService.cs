using Application.DTO;

namespace Application.Interfaces
{
    public interface IPermissionGroupService
    {
        PermissionGroupDTO GetPermissionGroup();

        PermissionGroupDTO FormPermissionGroup(int id);

        PermissionGroupDTO SavePermissionGroup(PermissionGroupDTO permissionGroupDTO);

        PermissionGroupDTO DeletePermissionGroup(int id);

        List<PermissionGroupDTO> GetList();
    }
}
