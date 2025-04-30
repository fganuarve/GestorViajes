using AutoMapper;
using GestorViajes.Models.EFCore.Rove;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestorViajes.Models.ViewModels.Trip;
using GestorViajes.Services.Trip;
using Microsoft.AspNetCore.Mvc;
using GestorViajes.Services.User;
using GestorViajes.Services.Vehicle;
using GestorViajes.Services.User.GestorViajes.Services.User;

namespace GestorViajes.Controllers
{

    public class TripController : Controller
    {
        private readonly ITripService _tripService;
        private readonly IVehicleService _vehicleService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public TripController(ITripService tripService, IMapper mapper, IVehicleService vehicleService, IUserService userService)
        {
            _tripService = tripService;
            _mapper = mapper;
            _vehicleService = vehicleService;
            _userService = userService;
        }

        [HttpGet]
        //Importante el metodo en el controlador debe coincidir con el nombre de la vista
        //no es Index! es IndexTrip!
        public async Task<IActionResult> IndexTrip()
        {
            var response = await _tripService.List();
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "No se pudieron cargar los viajes.";
                return View(new List<TripViewModel>());
            }

            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var response = await _tripService.Get(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "Viaje no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTrip()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null || !long.TryParse(userIdClaim.Value, out var userId))
            {
                TempData["status"] = "error";
                TempData["message"] = "No se pudo identificar al usuario.";
                return RedirectToAction("IndexTrip");
            }

            ViewBag.Vehicles = new SelectList(
                await _vehicleService.ListDropdownByUser(userId), "Id", "Description"
            );

            return View(new TripViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> CreateTrip(TripViewModel model)
        {
            var userId = long.Parse(User.FindFirst("UserId").Value);
            model.DriverId = userId; // Asignar el conductor actual

            if (!ModelState.IsValid)
            {
                TempData["status"] = "error";
                TempData["message"] = "Datos inválidos. Revisa los campos.";

                ViewBag.Vehicles = new SelectList(await _vehicleService.ListDropdownByUser(userId), "Id", "Description");
                return View(model);
            }

            var response = await _tripService.Add(model);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;

                ViewBag.Vehicles = new SelectList(await _vehicleService.ListDropdownByUser(userId), "Id", "Description");
                return View(model);
            }

            TempData["status"] = "success";
            TempData["message"] = "Viaje creado correctamente.";
            return RedirectToAction(nameof(IndexTrip));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var response = await _tripService.Get(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "Viaje no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> EditSubmit(TripViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var response = await _tripService.Update(model);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return View("Edit", model);
            }

            TempData["status"] = "success";
            TempData["message"] = "Viaje actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _tripService.Delete(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "No se pudo eliminar el viaje.";
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["message"] = "Viaje eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }        

        [HttpGet]
        public async Task<IActionResult> ByDriver(long driverId)
        {
            var response = await _tripService.ListByDriver(driverId);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "No se pudieron cargar los viajes del conductor.";
                return RedirectToAction(nameof(Index));
            }

            return View("Index", response.Data);
        }
    }
}


