using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Helper.Services.Interface;

namespace Gerenciador_de_Inventario_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IViewRenderService _viewRenderService;
        private ResponseDTO _responseDTO;

        public EmployeeController(IEmployeeService employeeService,
            IViewRenderService viewRenderService)
        {
            _employeeService = employeeService;
            _viewRenderService = viewRenderService;
            _responseDTO = new ResponseDTO();
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                EmployeeDTO employeeDTO = await _employeeService.GetEmployee();
                return View(employeeDTO);
            } catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Form(int id)
        {
            try
            {
                EmployeeDTO employeeDTO = await _employeeService.FormEmployee(id);
                _responseDTO.View = await _viewRenderService.RenderToStringAsync(this, "_Form", employeeDTO);
                return Json(_responseDTO);
            } catch (Exception)
            {
                _responseDTO.StatusErro = true;
                _responseDTO.Message = "Ops, tivemos um problema interno, não foi possível abrir o formulário!";
                return Json(_responseDTO);
            }
        }

        public async Task<IActionResult> Save(EmployeeDTO employeeDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.View = await _viewRenderService.RenderToStringAsync(this, "_Form", employeeDTO);
                    return Json(_responseDTO);
                }

                employeeDTO = await _employeeService.SaveEmploye(employeeDTO);
                if (employeeDTO.StatusErroMessage)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.Message = employeeDTO.Message;
                    return Json(_responseDTO);
                }

                _responseDTO.View = await _viewRenderService.RenderToStringAsync(this, "_TableEmployee", employeeDTO);
                return Json(_responseDTO);
            } catch (Exception)
            {
                _responseDTO.StatusErro = true;
                _responseDTO.Message = "Ops, tivemos um problema interno, não foi possível gravar sua solicitação!";
                return Json(_responseDTO);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                EmployeeDTO employeeDTO = await _employeeService.DeleteEmploye(id);
                if (employeeDTO.StatusErroMessage)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.Message = employeeDTO.Message;
                    return Json(_responseDTO);
                }

                _responseDTO.View = await _viewRenderService.RenderToStringAsync(this, "_TableEmployee", employeeDTO);
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
