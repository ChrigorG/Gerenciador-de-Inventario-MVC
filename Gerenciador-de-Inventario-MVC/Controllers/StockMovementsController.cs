using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Helper.Services.Interface;

namespace Gerenciador_de_Inventario_MVC.Controllers
{
    public class StockMovementsController : Controller
    {
        private readonly IStockMovementsService _stockMovementsService;

        public StockMovementsController(IStockMovementsService stockMovementsService)
        {
            _stockMovementsService = stockMovementsService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                StockMovementsDTO stockMovementsDTO = await _stockMovementsService.GetStockMovements();
                return View(stockMovementsDTO);
            } catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
