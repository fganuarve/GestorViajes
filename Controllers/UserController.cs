using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.User;
using GestorViajes.Services.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace GestorViajes.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        //Importante el metodo en el controlador debe coincidir con el nombre de la vista
        //no es Index! es IndexUser!!
        public async Task<IActionResult> IndexUser()
        {
            var response = await _userService.List();

            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error!.Message;
                return View(new List<UserViewModel>());
            }

            return View(response.Data);
        }

        //Uso claims para saber de que usuario que este logueado boy a obtener los detalles de la vista DetailsUser
        [HttpGet]
        public async Task<IActionResult> DetailsUser()
        {
            // Obtener ID del usuario autenticado desde los claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out var userId))
            {
                TempData["status"] = "error";
                TempData["message"] = "No se pudo determinar el usuario autenticado.";
                return RedirectToAction(nameof(Index));
            }

            var response = await _userService.GetById(userId);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "No se pudo obtener el usuario.";
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }


        [HttpGet]
        public IActionResult CreateUser()
        {
            return View(new UserViewModel());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUserSubmit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["status"] = "error";
                TempData["message"] = "Datos inválidos. Verifica e intenta de nuevo.";
                return View("Create", model);
            }

            var response = await _userService.Add(model);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return View("Create", model);
            }

            TempData["status"] = "success";
            TempData["message"] = "Usuario creado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(long id)
        {
            var response = await _userService.GetById(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserSubmit(UserViewModel model)
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
        public async Task<IActionResult> DeleteUser(long id)
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
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> DeactivateUser(long id)
        {
            var response = await _userService.Deactivate(id);
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
        [HttpPost]
        public async Task<IActionResult> ReactivateUser(long id)
        {
            var response = await _userService.Reactivate(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["message"] = "Usuario reactivado correctamente.";
            return RedirectToAction(nameof(Index));
        }

    }
}
