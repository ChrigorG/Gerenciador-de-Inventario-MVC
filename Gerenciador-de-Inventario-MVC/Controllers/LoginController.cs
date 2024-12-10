using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTO;

namespace Gerenciador_de_Inventario_MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Authenticate(LoginDTO loginDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeDTO employeeDTO = _loginService.Authenticate(loginDto);

                    if (employeeDTO.StatusErroMessage)
                    {
                        TempData["MessageErroLogin"] = employeeDTO.Message;
                        return View("Index", loginDto);
                    }

                    // Configurar as claims e autenticar o usuário
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, employeeDTO.Id.ToString()),
                        new Claim(ClaimTypes.Email, employeeDTO.Email),
                        new Claim(ClaimTypes.Name, employeeDTO.Name)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1) // Expira em 24hrs
                    };

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("Index", "Home");
                }

                return View("Index", loginDto);
            } catch (Exception)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos autenticar seus dados, tente novamente!";
                return RedirectToAction("Index");
            }
        }

        [AllowAnonymous]
        public IActionResult Logout()
        {
            // Limpa as informações de autenticação (sessão)
            HttpContext.Session.Clear();

            // Para remover todos os cookies da aplicação (Onde fica gravado o acesso)
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
