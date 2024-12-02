using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Helper.Services.Interface;

namespace Gerenciador_de_Inventario_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IViewRenderService _viewRenderService;
        private ResponseDTO _responseDTO;

        public ProductController(IProductService productService,
            IViewRenderService viewRenderService)
        {
            _productService = productService;
            _viewRenderService = viewRenderService;
            _responseDTO = new ResponseDTO();
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ProductDTO productDTO = await _productService.GetProduct();
                return View(productDTO);
            } catch (Exception)
            {
               return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Form(int id)
        {
            try
            {
                ProductDTO productDTO = await _productService.FormProduct(id);
                _responseDTO.View = await _viewRenderService.RenderToStringAsync(this, "_Form", productDTO);
                return Json(_responseDTO);
            } catch (Exception)
            {
                _responseDTO.StatusErro = true;
                _responseDTO.Message = "Ops, tivemos um problema interno, não foi possível abrir o formulário!";
                return Json(_responseDTO);
            }
        }

        public async Task<IActionResult> Save(ProductDTO productDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.View = await _viewRenderService.RenderToStringAsync(this, "_Form", productDTO);
                    return Json(_responseDTO);
                }

                productDTO = await _productService.SaveProduct(productDTO);
                if (productDTO.StatusErroMessage)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.Message = productDTO.Message;
                    return Json(_responseDTO);
                }

                _responseDTO.View = await _viewRenderService.RenderToStringAsync(this, "_TableProduct", productDTO);
                return Json(_responseDTO);
            } catch (Exception)
            {
                _responseDTO.StatusErro = true;
                _responseDTO.Message = "Ops, tivemos um problema interno, não foi possível gravar sua solicitação!";
                return Json(_responseDTO);
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                ProductDTO productDTO = await _productService.FormProduct(id);
                _responseDTO.View = await _viewRenderService.RenderToStringAsync(this, "_Detail", productDTO);
                return Json(_responseDTO);
            } catch (Exception)
            {
                _responseDTO.StatusErro = true;
                _responseDTO.Message = "Ops, tivemos um problema interno, não foi possível abrir o detalhar!";
                return Json(_responseDTO);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                ProductDTO productDTO = await _productService.DeleteProduct(id);
                if (productDTO.StatusErroMessage)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.Message = productDTO.Message;
                    return Json(_responseDTO);
                }

                _responseDTO.View = await _viewRenderService.RenderToStringAsync(this, "_TableProduct", productDTO);
                return Json(_responseDTO);
            } catch (Exception)
            {
                _responseDTO.StatusErro = true;
                _responseDTO.Message = "Ops, tivemos um problema interno, não foi possível atender sua solicitação";
                return Json(_responseDTO);
            }
        }
    }
}
