using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_de_Inventario_MVC.Controllers
{
    [Authorize]
    public class StockMovementsController : Controller
    {
        private readonly IStockMovementsService _stockMovementsService;

        public StockMovementsController(IStockMovementsService stockMovementsService)
        {
            _stockMovementsService = stockMovementsService;
        }

        public IActionResult Index()
        {
            try
            {
                StockMovementsDTO stockMovementsDTO = _stockMovementsService.GetStockMovements();
                return View(stockMovementsDTO);
            } catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
