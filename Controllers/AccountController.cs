using AutoMapper;
using GestorViajes.Models.ViewModels.User;
using GestorViajes.Services.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GestorViajes.Constants;

namespace GestorViajes.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserService _userService;

		public AccountController(
			IUserService userService
			)
		{
			_userService = userService;
		}

		// Muestra la vista AccountLogin.cshtml
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{   // Reenvia a AccountLogin si hay errores
				return View(nameof(Login), model);
			}

			var userResponse = await _userService.AuthenticateUserAsync(model.Email, model.Password);
			if (!userResponse.Success)
			{
				// Usamos TempData para pasar el mensaje a la vista
				TempData["message"] = userResponse.Error!.Message;
				TempData["status"] = "error";
				// Reenvia a AccountLogin si hay errores
				return View(nameof(Login), model);
			}

			// Autenticacion con cookies
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, userResponse.Data!.Name),
				new Claim(ClaimTypes.Email, userResponse.Data!.Email),
				new Claim(ClaimTypes.Role, userResponse.Data!.Role),
				new Claim(Settings.UserId, userResponse.Data!.Id.ToString())
			};

			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);
			//IMPORTANTE! usar contextAccesor!! y usando ! le digo que se que no es nulo
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

			TempData["message"] = "Inicio de sesión exitoso.";
			TempData["status"] = "success";
			// Redirige a IndexUser.cshtml de User  -> importante!!!
			return RedirectToAction("Index", "User");
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			// Cerrar sesion
			await HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			// Eliminar manualmente la cookie de autenticacin
			//utilizar context accesor!
			HttpContext!.Response.Cookies.Delete(".GestorViajes");

			return RedirectToAction(nameof(Login));
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View(new UserViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Register(UserViewModel model)
		{
			if (!ModelState.IsValid)
			{
				TempData["message"] = "Hubo un problema al crear tu cuenta. Por favor, verifica los datos.";
				TempData["status"] = "error";
				return View(model);
			}

			var response = await _userService.Add(model);

			// Verificamos si hubo algún error al crear el usuario
			if (!response.Success)
			{
				if(response.Error!.Message == "El email ya está en uso.")
				{
					TempData["message"] = $"{response.Error!.Message}";
					TempData["status"] = "error";
					return RedirectToAction(nameof(Login));
				}
				TempData["message"] = $"Hubo un problema al crear tu cuenta. {response.Error!.Message}";
				TempData["status"] = "error";  // Error
				return View(model);
			}

			// Si el usuario se ha registrado correctamente
			TempData["message"] = "Tu cuenta ha sido creada correctamente.";
			TempData["status"] = "success";  // Éxito

			// Redirigir a Login después de crear la cuenta
			return RedirectToAction(nameof(Login));
		}
	}
}
