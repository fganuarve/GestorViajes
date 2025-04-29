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

        // Esta acción mostrará la vista AccountLogin.cshtml
        [HttpGet]
        public IActionResult Login()
        {
            return View("AccountLogin"); // Asegúrate de que la vista se llame AccountLogin.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("AccountLogin", model); // Reenvía a AccountLogin si hay errores
            }

            var user = await _userService.GetUserByCredentialsAsync(model.Email, model.Password);

            if (user == null)
            {
                // Usamos TempData para pasar el mensaje a la vista
                TempData["message"] = "Correo electrónico o contraseña incorrectos.";
                TempData["status"] = "danger";
                return RedirectToAction("Login", "Account"); // Redirige a la acción Login
            }

            // Autenticación con cookies
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

            return RedirectToAction("Index", "Home"); // Redirige a Home después del login exitoso
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["message"] = "Has cerrado sesión correctamente.";
            TempData["status"] = "info";

            return RedirectToAction("Login", "Account"); // Redirige a Login tras cerrar sesión
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View("AccountLogin", model); // Si el modelo no es válido, se vuelve a mostrar la vista

            await _userService.Add(model);
            return RedirectToAction("Login", "Account"); // Redirige a Login tras el registro exitoso
        }
    }

}
