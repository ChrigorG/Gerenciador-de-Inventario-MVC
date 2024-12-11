using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Shared.Helper;
using System.Security.Claims;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPermissionGroupRepository _permissionGroupRepository;
        private readonly IPermissionGroupService _permissionGroupService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository,
            IPermissionGroupRepository permissionGroupRepository,
            IPermissionGroupService permissionGroupService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _permissionGroupRepository = permissionGroupRepository;
            _permissionGroupService = permissionGroupService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public EmployeeDTO GetEmployee()
        {
            EmployeeDTO? employeeDTO = new EmployeeDTO();
            string permission = this.GetPermission();
            if (permission == Constants.PermissionDenied)
            {
                employeeDTO.StatusErroMessage = true;
                employeeDTO.Message = "Acesso negado!";
                return employeeDTO;
            }

            employeeDTO.Title = "Funcionário";
            employeeDTO.ListEmployees = GetList();
            return employeeDTO;
        }

        public EmployeeDTO FormEmployee(int id)
        {
            EmployeeDTO? employeeDTO = new EmployeeDTO();

            string permission = this.GetPermission();
            if (permission != Constants.PermissionAccess)
            {
                employeeDTO.StatusErroMessage = true;
                employeeDTO.Message = "Acesso negado!";
                return employeeDTO;
            }

            // Se o id for nullo ou zero será tratado como um novo Funcionario
            if (Util.IsNullOrZero(id))
            {
                return new EmployeeDTO()
                {
                    Title = "Adicionar um Funcionário",
                    ListPermissionGroup = _permissionGroupService.GetList()
                };
            }

            // A partir daqui seria somente para atualização do funcionario
            Employee? employee = _employeeRepository.Get(id);
            Employee? employeeCreating = _employeeRepository.Get(employee?.IdUserCreated ?? 0);
            Employee? employeeUpdating = _employeeRepository.Get(employee?.IdUserUpdated ?? 0);
            employeeDTO = _mapper.Map<EmployeeDTO?>(employee);

            if (employeeDTO == null)
            {
                return NotFound(new EmployeeDTO());
            }

            employeeDTO.NameUserCreated = employeeCreating?.Name ?? "(Funcionario que criou foi desviculado do sistema)";
            employeeDTO.NameUserUpdated = employeeUpdating?.Name ?? "(Funcionario que atualizou foi desviculado do sistema)";
            employeeDTO.Title = $"Atualizar os dados do funcionário {employeeDTO.Id} - {employeeDTO.Name}";
            employeeDTO.ListPermissionGroup = _permissionGroupService.GetList();
            return employeeDTO;
        }

        public EmployeeDTO SaveEmploye(EmployeeDTO employeeDTO)
        {
            string permission = this.GetPermission();
            if (permission != Constants.PermissionAccess)
            {
                employeeDTO.StatusErroMessage = true;
                employeeDTO.Message = "Acesso negado!";
                return employeeDTO;
            }

            employeeDTO.ValidatedDTO();
            if (employeeDTO.StatusErroMessage)
            {
                return employeeDTO;
            }

            Employee? employee;
            // Adicionar um novo Funcionário
            if (Util.IsNullOrZero(employeeDTO.Id))
            {
                if (employeeDTO.Password.Length < 8 || employeeDTO.Password.Length > 70)
                {
                    employeeDTO.StatusErroMessage = true;
                    employeeDTO.Message = "A senha deve ter entre 8 a 70 caracteres";
                    return employeeDTO;
                }

                employee = _mapper.Map<Employee>(employeeDTO);
                employee.Password = Util.HashPassword(employee.Password);

                employee = _employeeRepository.Add(employee);
                if (employee == null)
                {
                    return InternalServerError(employeeDTO, $"salvar os dados do funcionário {employee!.Name}");
                }
            } else // Atualizar o Funcionário
            {
                employee = _employeeRepository.Get(employeeDTO.Id);
                if (employee == null)
                {
                    return NotFound(employeeDTO);
                }

                employee = ConvertDtoToModel(employee, employeeDTO);
                employee = _employeeRepository.Update(employee);
                if (employee == null)
                {
                    return InternalServerError(employeeDTO, $"atualizar o funcionário {employee!.Id} - {employee!.Name}");
                }
            }

            employeeDTO = _mapper.Map<EmployeeDTO>(employee);
            employeeDTO.Message = "Dados salvos com sucesso!";
            employeeDTO.ListEmployees = GetList();
            return employeeDTO;
        }

        public EmployeeDTO DeleteEmploye(int id)
        {
            EmployeeDTO? employeeDTO = new EmployeeDTO();
            string permission = this.GetPermission();
            if (permission == Constants.PermissionAccess)
            {
                employeeDTO.StatusErroMessage = true;
                employeeDTO.Message = "Acesso negado!";
                return employeeDTO;
            }

            Employee? employee = _employeeRepository.Get(id);

            if (employee == null)
            {
                return NotFound(new EmployeeDTO());
            }

            employee = _employeeRepository.Delete(employee);
            if (employee == null)
            {
                return InternalServerError(new EmployeeDTO(), $"deletar o funcionário {employee!.Id} - {employee!.Name}");
            }

            employeeDTO = _mapper.Map<EmployeeDTO>(employee);
            employeeDTO.ListEmployees = GetList();
            return employeeDTO;
        }

        private List<EmployeeDTO> GetList()
        {
            List<Employee> employees = _employeeRepository.Get();
            List<EmployeeDTO> employeeDTOs = _mapper.Map<List<EmployeeDTO>>(employees);

            List<PermissionGroup> permissionGroups = _permissionGroupRepository.Get();

            employeeDTOs = (from employee in employeeDTOs
                            join permissionGroup in permissionGroups on employee.IdPermissionGroup equals permissionGroup.Id
                            select new EmployeeDTO
                            {
                                Id = employee.Id,
                                StatusDeleted = employee.StatusDeleted,
                                IdUserCreated = employee.IdUserCreated,
                                DatetimeCreate = employee.DatetimeCreate,
                                IdUserUpdated = employee.IdUserUpdated,
                                DatetimeUpdated = employee.DatetimeUpdated,
                                IdPermissionGroup = employee.IdPermissionGroup,
                                Name = employee.Name,
                                Function = employee.Function,
                                Email = employee.Email,
                                Status = employee.Status,
                                permissionGroupDTO = _mapper.Map<PermissionGroupDTO>(permissionGroup)
                            }).ToList();
            return employeeDTOs;
        }

        private string GetPermission()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = 0;
            if (userIdClaim != null && int.TryParse(userIdClaim!.Value, out userId))
            {
                Employee? employee = _employeeRepository.Get(userId);
                PermissionGroup? permissionGroup = _permissionGroupRepository.Get(employee?.IdPermissionGroup ?? 0);

                return permissionGroup?.ActionEmployee ?? Constants.PermissionDenied;
            }
            return Constants.PermissionDenied;
        }

        private Employee ConvertDtoToModel(Employee employee, EmployeeDTO employeeDTO)
        {
            employee.IdPermissionGroup = employeeDTO.IdPermissionGroup;
            employee.Function = employeeDTO.Function;
            employee.Status = employeeDTO.Status;
            employee.Email = employeeDTO.Email;
            employee.Name = employeeDTO.Name;

            return employee;
        }

        private EmployeeDTO NotFound(EmployeeDTO employeeDTO)
        {
            employeeDTO.StatusErroMessage = true;
            employeeDTO.Message = "Nenhum funcinário encontrado!";
            return employeeDTO;
        }

        private EmployeeDTO InternalServerError(EmployeeDTO employeeDTO, string complementMessage)
        {
            employeeDTO.StatusErroMessage = true;
            employeeDTO.Message = $"Ops, não conseguimos {complementMessage}, tente mais tarde!";
            return employeeDTO;
        }
    }
}
