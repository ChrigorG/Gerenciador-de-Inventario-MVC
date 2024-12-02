using Application.DTO;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Helper.Services.Interface;

namespace Gerenciador_de_Inventario_MVC.Controllers
{
    public class PermissionGroupController : Controller
    {
        private readonly IPermissionGroupService _permissionGroupService;
        private readonly IViewRenderService _viewRenderService;
        private ResponseDTO _responseDTO;

        public PermissionGroupController(IPermissionGroupService permissionGroupService,
            IViewRenderService viewRenderService)
        {
            _permissionGroupService = permissionGroupService;
            _viewRenderService = viewRenderService;
            _responseDTO = new ResponseDTO();
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                PermissionGroupDTO permissionGroupDTO = await _permissionGroupService.GetPermissionGroup();
                return View(permissionGroupDTO);
            } catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Form(int id)
        {
            try
            {
                PermissionGroupDTO permissionGroupDTO = await _permissionGroupService.FormPermissionGroup(id);
                _responseDTO.View = await _viewRenderService.RenderToStringAsync(this, "_Form", permissionGroupDTO);
                return Json(_responseDTO);
            } catch (Exception)
            {
                _responseDTO.StatusErro = true;
                _responseDTO.Message = "Ops, tivemos um problema interno, não foi possível abrir o formulário!";
                return Json(_responseDTO);
            }
        }

        public async Task<IActionResult> Save(PermissionGroupDTO permissionGroupDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.View = await _viewRenderService.RenderToStringAsync(this, "_Form", permissionGroupDTO);
                    return Json(_responseDTO);
                }

                permissionGroupDTO = await _permissionGroupService.SavePermissionGroup(permissionGroupDTO);
                if (permissionGroupDTO.StatusErroMessage)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.Message = permissionGroupDTO.Message;
                    return Json(_responseDTO);
                }

                _responseDTO.View = await _viewRenderService.RenderToStringAsync(this, "_TablePermissionGroup", permissionGroupDTO);
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
                PermissionGroupDTO permissionGroupDTO = await _permissionGroupService.FormPermissionGroup(id);
                _responseDTO.View = await _viewRenderService.RenderToStringAsync(this, "_Detail", permissionGroupDTO);
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
                PermissionGroupDTO permissionGroupDTO = await _permissionGroupService.DeletePermissionGroup(id);
                if (permissionGroupDTO.StatusErroMessage)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.Message = permissionGroupDTO.Message;
                    return Json(_responseDTO);
                }

                _responseDTO.View = await _viewRenderService.RenderToStringAsync(this, "_TablePermissionGroup", permissionGroupDTO);
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
