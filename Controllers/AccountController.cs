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

        // Muestra la vista AccountLogin.cshtml
        [HttpGet]
        public IActionResult Login()
        {   
            return View("AccountLogin");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {   // Reenvia a AccountLogin si hay errores
                return View("AccountLogin", model);
            }

            var user = await _userService.GetUserByCredentialsAsync(model.Email, model.Password);

            if (user == null)
            {
                // Usamos TempData para pasar el mensaje a la vista
                TempData["message"] = "Correo electrónico o contraseña incorrectos.";
                TempData["status"] = "danger";
                // Redirige a la accion Login
                return RedirectToAction("Login", "Account");
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
            // Redirige a Home despues del login exitoso
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["message"] = "Has cerrado sesión correctamente.";
            TempData["status"] = "info";
            // Redirige a Login tras cerrar sesion
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AccountRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AccountRegister(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Verificar si ya existe un usuario con ese email
            var existingUser = await _userService.GetUserByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Ya existe una cuenta registrada con este correo.");
                return View(model);
            }

            // Registrar usuario
            var response = await _userService.Add(model);

            if (response.Error != null)
            {
                ModelState.AddModelError("", "Hubo un error al registrar el usuario: " + response.Error.Message);
                return View(model);
            }

            // Redirigir al login tras registro exitoso
            return RedirectToAction("Login", "Account");
        }

    }

}
