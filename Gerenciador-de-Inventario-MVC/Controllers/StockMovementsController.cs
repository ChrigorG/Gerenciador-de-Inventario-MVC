using Application.DTO;
using Application.Interfaces;
using Data.Repositories;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Helper.Services.Interface;

namespace Gerenciador_de_Inventario_MVC.Controllers
{
    [Authorize]
    public class StockMovementsController : Controller
    {
        private readonly IStockMovementsService _stockMovementsService;
        private readonly IViewRenderService _viewRenderService;
        private readonly IEmployeeRepository _employeeRepository;
        private ResponseDTO _responseDTO;

        public StockMovementsController(IStockMovementsService stockMovementsService,
            IEmployeeRepository employeeRepository,
            IViewRenderService viewRenderService)
        {
            _stockMovementsService = stockMovementsService;
            _employeeRepository = employeeRepository;
            _viewRenderService = viewRenderService;
            _responseDTO = new ResponseDTO();
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

        public IActionResult Form()
        {
            try
            {
                StockMovementsDTO stockMovementsDTO = _stockMovementsService.FormProductInStock();
                _responseDTO.View = _viewRenderService.RenderToString(this, "_Form", stockMovementsDTO);
                return Json(_responseDTO);
            } catch (Exception)
            {
                _responseDTO.StatusErro = true;
                _responseDTO.Message = "Ops, tivemos um problema interno, não foi possível abrir o formulário!";
                return Json(_responseDTO);
            }
        }

        public IActionResult FormRemove()
        {
            try
            {
                StockMovementsDTO stockMovementsDTO = _stockMovementsService.FormRemoveProductInStock();
                _responseDTO.StatusErro = false;
                _responseDTO.View = _viewRenderService.RenderToString(this, "_FormRemove", stockMovementsDTO);
                return Json(_responseDTO);
            } catch (Exception)
            {
                _responseDTO.StatusErro = true;
                _responseDTO.Message = "Ops, tivemos um problema interno, não foi possível abrir o formulário!";
                return Json(_responseDTO);
            }
        }

        public IActionResult Save(StockMovementsDTO stockMovementsDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.View = _viewRenderService.RenderToString(this, "_PartialForm", stockMovementsDTO);
                    return Json(_responseDTO);
                }

                stockMovementsDTO = _stockMovementsService.AddProductInStock(stockMovementsDTO);
                if (stockMovementsDTO.StatusErroMessage)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.Message = stockMovementsDTO.Message;
                    return Json(_responseDTO);
                }

                _responseDTO.Message = stockMovementsDTO.Message;
                _responseDTO.View = _viewRenderService.RenderToString(this, "_TableStockMovements", stockMovementsDTO);
                return Json(_responseDTO);
            } catch (Exception)
            {
                _responseDTO.StatusErro = true;
                _responseDTO.Message = "Ops, tivemos um problema interno, não foi possível gravar sua solicitação!";
                return Json(_responseDTO);
            }
        }

        public IActionResult Remove(StockMovementsDTO stockMovementsDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.View = _viewRenderService.RenderToString(this, "_PartialFormRemove", stockMovementsDTO);
                    return Json(_responseDTO);
                }

                stockMovementsDTO = _stockMovementsService.RemoveProductInStock(stockMovementsDTO);
                if (stockMovementsDTO.StatusErroMessage)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.Message = stockMovementsDTO.Message;
                    return Json(_responseDTO);
                }

                _responseDTO.Message = stockMovementsDTO.Message;
                _responseDTO.View = _viewRenderService.RenderToString(this, "_TableStockMovements", stockMovementsDTO);
                return Json(_responseDTO);
            } catch (Exception)
            {
                _responseDTO.StatusErro = true;
                _responseDTO.Message = "Ops, tivemos um problema interno, não foi possível gravar sua solicitação!";
                return Json(_responseDTO);
            }
        }

        public IActionResult Detail(int id)
        {
            try
            {
                StockMovementsDTO stockMovementsDTO = _stockMovementsService.DetailProductInStock(id);
                if (stockMovementsDTO.StatusErroMessage)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.Message = stockMovementsDTO.Message;
                    return Json(_responseDTO);
                }

                _responseDTO.View = _viewRenderService.RenderToString(this, "_Detail", stockMovementsDTO);
                return Json(_responseDTO);
            } catch (Exception)
            {
                _responseDTO.StatusErro = true;
                _responseDTO.Message = "Ops, tivemos um problema interno, não foi possível abrir o formulário!";
                return Json(_responseDTO);
            }
        }
    }
}
