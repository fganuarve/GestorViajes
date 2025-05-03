using AutoMapper;
using GestorViajes.Models.ViewModels;
using GestorViajes.Models.ViewModels.Image;
using GestorViajes.Services.FuelTicket;
using GestorViajes.Services.Image;
using GestorViajes.Services.Users;
using GestorViajes.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorViajes.Controllers
{
    [Authorize]
    public class FuelTicketController : Controller
    {
        private readonly IFuelTicketService _fuelTicketService;
        private readonly IUserService _userService;
		public FuelTicketController(
            IFuelTicketService fuelTicketService, 
            IUserService userService
            )
        {
            _fuelTicketService = fuelTicketService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var current = await _userService.CurrentUser();
			var response = await _fuelTicketService.List(x=> x.UserId == current.Id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "No se pudieron cargar los tickets.";
                return View(new List<FuelTicketViewModel>());
            }

            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var response = await _fuelTicketService.Get(id, true);
            if (!response.Success || response.Data == null)
            {
                TempData["status"] = "error";
                TempData["message"] = "Ticket no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new FuelTicketViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FuelTicketViewModel model)
        {
			if (model.ImageFile == null || model.ImageFile.Length == 0)
			{
				TempData["Error"] = "Por favor, suba una imagen al ticket.";
				return RedirectToAction(nameof(Create));
			}

			var response = await _fuelTicketService.Add(model);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "Error al registrar el ticket.";
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["message"] = "Ticket registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var response = await _fuelTicketService.Get(id, true);
            if (!response.Success || response.Data == null)
            {
                TempData["status"] = "error";
                TempData["message"] = "Ticket no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FuelTicketViewModel model)
        {
			if (model.ImageFile == null || model.ImageFile.Length == 0)
			{
				TempData["Error"] = "Por favor, suba una imagen al ticket.";
				return RedirectToAction(nameof(Create));
			}

			var response = await _fuelTicketService.Update(model);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "Error al actualizar el ticket.";
                return RedirectToAction(nameof(Edit), model);
            }

            TempData["status"] = "success";
            TempData["message"] = "Ticket actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _fuelTicketService.Delete(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "Error al eliminar el ticket.";
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["message"] = "Ticket eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
