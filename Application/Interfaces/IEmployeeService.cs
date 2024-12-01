using Application.DTO;

namespace Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> GetEmployee();

        Task<EmployeeDTO> FormEmployee(int id);

        Task<EmployeeDTO> SaveEmploye(EmployeeDTO employeeDTO);

        Task<EmployeeDTO> DeleteEmploye(int id);
    }
}
