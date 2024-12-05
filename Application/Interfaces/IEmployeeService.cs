using Application.DTO;

namespace Application.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDTO GetEmployee();

        EmployeeDTO FormEmployee(int id);

        EmployeeDTO SaveEmploye(EmployeeDTO employeeDTO);

        EmployeeDTO DeleteEmploye(int id);
    }
}
