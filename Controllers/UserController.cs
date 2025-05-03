using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using GestorViajes.Models.ViewModels.User;
using GestorViajes.Services.Users;
using Microsoft.AspNetCore.Authorization;
using GestorViajes.Models.EFCore.Rove;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using GestorViajes.Services.Trips;
using GestorViajes.Constants;

namespace GestorViajes.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ITripService _tripService;
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
        public UserController(
            IUserService userService,
			ITripService tripService,
			IMapper mapper
            )
        {
			_userService = userService;
			_tripService = tripService;
            _mapper = mapper;
        }

        [HttpGet]
        //Importante el metodo en el controlador debe coincidir con el nombre de la vista
        public async Task<IActionResult> Index()
        {
            var current = await _userService.CurrentUser();
            if(current == null)
            {
                // el authorize garantiza que esta logueado
                var userId = long.Parse(User?.FindFirst(Settings.UserId).Value);
                var exists = await _userService.Get(x => x.Id == userId);
				if (!exists.Success)
				{
					TempData["status"] = "error";
					TempData["message"] = exists.Error!.Message;
                    return RedirectToAction("Register", "Account");
				}
                if(exists.Data == null)
                {
					return RedirectToAction("Register", "Account");
				}
                else
                {
					TempData["status"] = "error";
					TempData["message"] = "Su cuenta está inactiva, contacte con un administrador para reactivarla.";
                    // forzamos el borrado de la cookie
					await HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
					HttpContext!.Response.Cookies.Delete(".GestorViajes");
					return RedirectToAction("Login", "Account");
				}
			}


			var response = await _tripService.List(x => x.DriverId == current!.Id || x.Passengers.Select(y => y.UserId).Contains(current.Id));

            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error!.Message;
                return View(new List<UserViewModel>());
            }

            return View(response.Data);
        }

        //Uso contextAccesor para saber de que usuario que este logueado voy a obtener los detalles de la vista DetailsUser
        //Se almacena el ID del usuario autenticado en un claim personalizado llamado "UserId"
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var current = await _userService.CurrentUser();
            var model = _mapper.Map<UserViewModel>(current);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var response = await _userService.Get(x => x.Id == id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var response = await _userService.Update(model);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return View("Edit", model);
            }

            TempData["status"] = "success";
            TempData["message"] = "Usuario actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _userService.Delete(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["message"] = "Usuario eliminado correctamente.";

            // Cerrar sesion y eliminar la cookie si se trata del usuario logueado
            var current = await _userService.CurrentUser();
            if(current == null)
            {
				await HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
				HttpContext!.Response.Cookies.Delete(".GestorViajes");
				return RedirectToAction("Login", "Account");
			}
			return RedirectToAction(nameof(Index));
		}

        //Boton para activar o desactivar
        [HttpPost]
        public async Task<IActionResult> Toggle()
        {
            var current = await _userService.CurrentUser();
            var response = await _userService.Toggle(current.Id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["message"] = "Usuario desactivado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        //Boton para cambiar la suscripcion
        //Solo hay dos estados asi que cambia de una suscripciojn a otra
        [HttpPost]
        public async Task<IActionResult> ChangeSubscription(int idSubscription = 1)
        {
            var current = await _userService.CurrentUser();
            var subscriptionType = (SubscriptionType)idSubscription;
            var response = await _userService.ChangeSubscriptionAsync(current!.Id, subscriptionType);

            TempData["status"] = response.Success ? "success" : "danger";
            TempData["message"] = response.Success
                ? "Suscripción actualizada correctamente."
                : $"Ocurrió un error al actualizar su suscripción. {response.Error?.Message}";

            return RedirectToAction(nameof(Index));
        }
    }
}
