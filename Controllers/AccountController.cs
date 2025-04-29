using AutoMapper;
using GestorViajes.Models.ViewModels.User;
using GestorViajes.Services.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace GestorViajes.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userService.GetUserByCredentialsAsync(model.Email, model.Password);

            if (user == null)
            {
                // Usamos TempData para pasar el mensaje a la vista
                TempData["message"] = "Correo electrónico o contraseña incorrectos.";
                // Se puede usar "success", "info", "warning" con Bootstrap u otro CSS
                TempData["status"] = "danger";
                return RedirectToAction("Login");
            }

            // Autenticacion con cookies
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("UserId", user.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            TempData["message"] = "Inicio de sesión exitoso.";
            TempData["status"] = "success";

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["message"] = "Has cerrado sesión correctamente.";
            TempData["status"] = "info";

            return RedirectToAction("Login", "Account");
        }
    }

}
