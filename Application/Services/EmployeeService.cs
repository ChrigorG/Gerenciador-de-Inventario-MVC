using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Helper;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> GetEmployee()
        {
            return new EmployeeDTO()
            {
                Title = "Funcionário",
                ListEmployees = await GetList()
            };
        }

        public async Task<EmployeeDTO> FormEmployee(int id)
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
            Employee? employee = await _employeeRepository.Get(id);
            EmployeeDTO? employeeDTO = _mapper.Map<EmployeeDTO?>(employee);

            if (employeeDTO == null)
            {
                return NotFound();
            }

            employeeDTO.Title = $"Atualizar os dados do funcionário {employeeDTO.Id} - {employeeDTO.Name}";
            return employeeDTO;
        }

        public async Task<EmployeeDTO> SaveEmploye(EmployeeDTO employeeDTO)
        {
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
                employee = await _employeeRepository.Add(employee);
                if (employee == null)
                {
                    return InternalServerError($"salvar os dados do funcionário {employee!.Name}");
                }
            } else // Atualizar o Funcionário
            {
                employee = await _employeeRepository.Update(employee);
                if (employee == null)
                {
                    return InternalServerError($"atualizar o funcionário {employee!.Id} - {employee!.Name}");
                }
            }

            employeeDTO = _mapper.Map<EmployeeDTO>(employee);
            employeeDTO.ListEmployees = await GetList();
            return employeeDTO;
        }

        public async Task<EmployeeDTO> DeleteEmploye(int id)
        {
            Employee? employee = await _employeeRepository.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee = await _employeeRepository.Delete(employee);
            if (employee == null)
            {
                return InternalServerError($"deletar o funcionário {employee!.Id} - {employee!.Name}");
            }

            EmployeeDTO employeeDTO = _mapper.Map<EmployeeDTO>(employee);
            employeeDTO.ListEmployees = await GetList();
            return employeeDTO;
        }

        private async Task<List<EmployeeDTO>> GetList()
        {
            List<Employee> employees = await _employeeRepository.Get();
            return _mapper.Map<List<EmployeeDTO>>(employees);
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
