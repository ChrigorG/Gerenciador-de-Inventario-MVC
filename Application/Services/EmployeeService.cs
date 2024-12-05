using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Helper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Web.Helpers;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPermissionGroupRepository _permissionGroupRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository,
            IPermissionGroupRepository permissionGroupRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _permissionGroupRepository = permissionGroupRepository;
            _mapper = mapper;
        }

        public EmployeeDTO GetEmployee()
        {
            return new EmployeeDTO()
            {
                Title = "Funcionário",
                ListEmployees = GetList()
            };
        }

        public EmployeeDTO FormEmployee(int id)
        {
            // Se o id for nullo ou zero será tratado como um novo Funcionario
            if (Util.IsNullOrZero(id))
            {
                return new EmployeeDTO()
                {
                    Title = "Adicionar um Funcionário"
                };
            }

            // A partir daqui seria somente para atualização do funcionario
            Employee? employee = _employeeRepository.Get(id);
            EmployeeDTO? employeeDTO = _mapper.Map<EmployeeDTO?>(employee);

            if (employeeDTO == null)
            {
                return NotFound();
            }

            employeeDTO.Title = $"Atualizar os dados do funcionário {employeeDTO.Id} - {employeeDTO.Name}";
            return employeeDTO;
        }

        public EmployeeDTO SaveEmploye(EmployeeDTO employeeDTO)
        {
            employeeDTO.ValidatedDTO();

            if (employeeDTO.StatusErroMessage)
            {
                return employeeDTO;
            }

            Employee? employee = _mapper.Map<Employee>(employeeDTO);
            var validationResults = ValidationEntities.Validate(employee);

            if (validationResults.Count > 0)
            {
                foreach (var error in validationResults)
                {
                    employeeDTO.Message += $"{error.ErrorMessage}\n";
                }

                employeeDTO.StatusErroMessage = false;
                return employeeDTO;
            }

            // Adicionar um novo Funcionário
            if (Util.IsNullOrZero(employee.Id))
            {
                employee = _employeeRepository.Add(employee);
                if (employee == null)
                {
                    return InternalServerError($"salvar os dados do funcionário {employee!.Name}");
                }
            } else // Atualizar o Funcionário
            {
                employee = _employeeRepository.Update(employee);
                if (employee == null)
                {
                    return InternalServerError($"atualizar o funcionário {employee!.Id} - {employee!.Name}");
                }
            }

            employeeDTO = _mapper.Map<EmployeeDTO>(employee);
            employeeDTO.ListEmployees = GetList();
            return employeeDTO;
        }

        public EmployeeDTO DeleteEmploye(int id)
        {
            Employee? employee = _employeeRepository.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee = _employeeRepository.Delete(employee);
            if (employee == null)
            {
                return InternalServerError($"deletar o funcionário {employee!.Id} - {employee!.Name}");
            }

            EmployeeDTO employeeDTO = _mapper.Map<EmployeeDTO>(employee);
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

        private EmployeeDTO NotFound()
        {
            return new EmployeeDTO()
            {
                StatusErroMessage = true,
                Message = "Nenhum funcinário encontrado!"
            };
        }

        private EmployeeDTO InternalServerError(string complementMessage)
        {
            return new EmployeeDTO()
            {
                StatusErroMessage = true,
                Message = $"Ops, não conseguimos {complementMessage}, tente mais tarde!"
            };
        }

    }
}
