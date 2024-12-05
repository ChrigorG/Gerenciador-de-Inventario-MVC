using Domain.Entities;
using Domain.Interfaces;
using Shared.Helper;

namespace Application.Services
{
    public class InitDbService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPermissionGroupRepository _permissionGroupRepository;

        public InitDbService(IEmployeeRepository employeeRepository,
                IPermissionGroupRepository permissionGroupRepository)
        {
            _employeeRepository = employeeRepository;
            _permissionGroupRepository = permissionGroupRepository;
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
                    IdUserCreated = 0,
                    ActionEmployee = Constants.PermissionAccess,
                    ActionPermissionGroup = Constants.PermissionAccess,
                    ActionProduct = Constants.PermissionAccess,
                    ActionStockMovements = Constants.PermissionAccess,
                };
                _ = _permissionGroupRepository.Add(permissionGroup);

                if (!_employeeRepository.Any())
                {
                    Employee employee = new Employee()
                    {
                        Name = "Administrador",
                        Function = "Adm",
                        IdUserCreated = 0,
                        IdPermissionGroup = 1,
                        Email = "chrigorcontato@gmail.com",
                        Password = Util.HashPassword("@dm2024"),
                        Status = true,
                    };
                    _ = _employeeRepository.Add(employee);
                }
            }
        }
    }
}
