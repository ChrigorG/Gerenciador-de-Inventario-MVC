using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Helper;

namespace Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public LoginService(IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public EmployeeDTO Authenticate(LoginDTO loginDTO)
        {
            string email = loginDTO.Email;
            string password = Util.HashPassword(loginDTO.Password);
            Employee? employee = _employeeRepository.ValidatedUser(email, password);
            EmployeeDTO employeeDTO = new EmployeeDTO();

            if (employee == null)
            {
                employeeDTO.StatusErroMessage = true;
                employeeDTO.Message = "Dados inválidos.";
                return employeeDTO;

            } else if (employee.Status == false)
            {
                employeeDTO.StatusErroMessage = true;
                employeeDTO.Message = "Acesso negado, o acesso à sua conta foi restringido.";
                return employeeDTO;
            }

            employeeDTO = _mapper.Map<EmployeeDTO>(employee);
            return employeeDTO;
        }
    }
}
