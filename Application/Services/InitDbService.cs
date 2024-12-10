using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Shared.Helper;

namespace Application.Services
{
    public class InitDbService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPermissionGroupRepository _permissionGroupRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InitDbService(IEmployeeRepository employeeRepository,
                IPermissionGroupRepository permissionGroupRepository,
                IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository = employeeRepository;
            _permissionGroupRepository = permissionGroupRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public void InitDb()
        {
            if (!_permissionGroupRepository.Any())
            {
                PermissionGroup permissionGroup = new PermissionGroup()
                {
                    Name = "Administrador",
                    Description = "Grupo de Permissão criado para ser o Administrador geral da Aplicação...",
                    Status = true,
                    ActionEmployee = Constants.PermissionAccess,
                    ActionPermissionGroup = Constants.PermissionAccess,
                    ActionProduct = Constants.PermissionAccess,
                    ActionStockMovements = Constants.PermissionAccess,
                };
                _ = _permissionGroupRepository.Add(permissionGroup, 0);

                if (!_employeeRepository.Any())
                {
                    Employee employee = new Employee()
                    {
                        Name = "Administrador",
                        Function = "Adm",
                        IdPermissionGroup = permissionGroup.Id,
                        Email = "adm@gerenciadorinventario.com",
                        Password = Util.HashPassword("#dm2024"),
                        Status = true,
                    };
                    _ = _employeeRepository.Add(employee, 0);
                }
            }
        }
    }
}
