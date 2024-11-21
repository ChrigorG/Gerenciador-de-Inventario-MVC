using Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_de_Inventario_MVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View(new ProductDTO());
        }
    }
}
