using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Helper.Services.Interface;

namespace Gerenciador_de_Inventario_MVC.Controllers
{
    [Authorize]
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

        public IActionResult Index()
        {
            try
            {
                PermissionGroupDTO permissionGroupDTO = _permissionGroupService.GetPermissionGroup();
                return View(permissionGroupDTO);
            } catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Form(int id)
        {
            try
            {
                PermissionGroupDTO permissionGroupDTO = _permissionGroupService.FormPermissionGroup(id);
                _responseDTO.View = _viewRenderService.RenderToString(this, "_Form", permissionGroupDTO);
                return Json(_responseDTO);
            } catch (Exception)
            {
                _responseDTO.StatusErro = true;
                _responseDTO.Message = "Ops, tivemos um problema interno, não foi possível abrir o formulário!";
                return Json(_responseDTO);
            }
        }

        public IActionResult Save(PermissionGroupDTO permissionGroupDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.View = _viewRenderService.RenderToString(this, "_Form", permissionGroupDTO);
                    return Json(_responseDTO);
                }

                permissionGroupDTO = _permissionGroupService.SavePermissionGroup(permissionGroupDTO);
                if (permissionGroupDTO.StatusErroMessage)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.Message = permissionGroupDTO.Message;
                    return Json(_responseDTO);
                }

                _responseDTO.View = _viewRenderService.RenderToString(this, "_TablePermissionGroup", permissionGroupDTO);
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
                PermissionGroupDTO permissionGroupDTO = _permissionGroupService.FormPermissionGroup(id);
                _responseDTO.View = _viewRenderService.RenderToString(this, "_Detail", permissionGroupDTO);
                return Json(_responseDTO);
            } catch (Exception)
            {
                _responseDTO.StatusErro = true;
                _responseDTO.Message = "Ops, tivemos um problema interno, não foi possível abrir o detalhar!";
                return Json(_responseDTO);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                PermissionGroupDTO permissionGroupDTO = _permissionGroupService.DeletePermissionGroup(id);
                if (permissionGroupDTO.StatusErroMessage)
                {
                    _responseDTO.StatusErro = true;
                    _responseDTO.Message = permissionGroupDTO.Message;
                    return Json(_responseDTO);
                }

                _responseDTO.View = _viewRenderService.RenderToString(this, "_TablePermissionGroup", permissionGroupDTO);
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
