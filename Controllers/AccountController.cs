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
            return View(new UserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AccountRegister(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Add(model);

                // Verificamos si hubo algún error al crear el usuario
                if (response.Error != null)
                {
                    TempData["message"] = "Hubo un problema al crear tu cuenta. Por favor, intenta nuevamente.";
                    TempData["status"] = "danger";  // Error
                    return View(model);
                }

                // Si el usuario se ha registrado correctamente
                TempData["message"] = "Tu cuenta ha sido creada correctamente.";
                TempData["status"] = "success";  // Éxito

                // Redirigir a Login después de crear la cuenta
                return RedirectToAction("Login", "Account");
            }

            // Si el modelo no es válido, mostrar el mensaje de error
            TempData["message"] = "Hubo un problema al crear tu cuenta. Por favor, verifica los datos.";
            TempData["status"] = "danger";  // Error
            return View(model);
        }



    }

}
