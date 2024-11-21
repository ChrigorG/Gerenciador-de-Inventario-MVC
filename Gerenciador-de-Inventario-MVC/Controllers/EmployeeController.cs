using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_de_Inventario_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
