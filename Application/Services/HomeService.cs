using Application.DTO;
using Application.Interfaces;

namespace Application.Services
{
    public class HomeService : IHomeService
    {
        public HomeService() 
        { 
        
        }

        public HomeDTO GetData()
        {
            return new HomeDTO();
        }
    }
}
