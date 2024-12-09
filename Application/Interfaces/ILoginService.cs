using Application.DTO;

namespace Application.Interfaces
{
    public interface ILoginService
    {
        EmployeeDTO Authenticate(LoginDTO loginDTO);
    }
}
