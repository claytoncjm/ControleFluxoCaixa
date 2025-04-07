using ControleFluxoCaixa.Models;
using ControleFluxoCaixa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace ControleFluxoCaixa.Controllers
{
    // Controller Pattern - Separação de responsabilidades MVC
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Transaction");
            }
            return View();
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Transaction");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var token = await _authService.LoginAsync(model);
                if (token == null)
                {
                    ModelState.AddModelError(string.Empty, "Erro de usuário ou senha");
                    return View(model);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Transaction");
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Usuário inexistente");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var token = await _authService.RegisterAsync(model);
                TempData["SuccessMessage"] = "Usuário registrado com sucesso!";
                return RedirectToAction("Login");
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
