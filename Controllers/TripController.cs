using AutoMapper;
using GestorViajes.Models.EFCore.Rove;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestorViajes.Models.ViewModels.Trip;
using GestorViajes.Services.Trip;
using Microsoft.AspNetCore.Mvc;
using GestorViajes.Services.User;
using GestorViajes.Services.Vehicle;

namespace GestorViajes.Controllers
{
    public class TripController : Controller
    {
        /*private readonly ITripService _tripService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IVehicleService _vehicleService;

        public TripController(
            ITripService tripService,
            IMapper mapper,
            IUserService userService,
            IVehicleService vehicleService)
        {
            _tripService = tripService;
            _mapper = mapper;
            _userService = userService;
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _tripService.List();
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "Error al cargar los viajes.";
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
                TempData["message"] = response.Error?.Message ?? "No se encontró el viaje.";
                return RedirectToAction(nameof(Index));
            }

            var tripViewModel = _mapper.Map<TripViewModel>(response.Data);
            return View(tripViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var usersResponse = await _userService.List();
            var vehiclesResponse = await _vehicleService.List();

            if (!usersResponse.Success || !vehiclesResponse.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = "Error al cargar datos para crear viaje.";
                return RedirectToAction(nameof(Index));
            }

            var model = new TripViewModel
            {
                Drivers = usersResponse.Data.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.Name} {u.LastName1}"
                }).ToList(),

                Vehicles = vehiclesResponse.Data.Select(v => new SelectListItem
                {
                    Value = v.Id.ToString(),
                    Text = v.Plate
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubmit(TripViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["status"] = "error";
                TempData["message"] = "Datos inválidos al crear el viaje.";
                return RedirectToAction(nameof(Create));
            }

            var response = await _tripService.Add(model);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "Error al crear viaje.";
                return RedirectToAction(nameof(Create));
            }

            TempData["status"] = "success";
            TempData["message"] = "Viaje creado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var response = await _tripService.Get(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "Error al cargar el viaje.";
                return RedirectToAction(nameof(Index));
            }

            var tripViewModel = _mapper.Map<TripViewModel>(response.Data);

            // Cargar listas de drivers y vehículos para el dropdown
            var usersResponse = await _userService.List();
            var vehiclesResponse = await _vehicleService.List();

            if (usersResponse.Success)
            {
                tripViewModel.Drivers = usersResponse.Data.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.Name} {u.LastName1}"
                }).ToList();
            }

            if (vehiclesResponse.Success)
            {
                tripViewModel.Vehicles = vehiclesResponse.Data.Select(v => new SelectListItem
                {
                    Value = v.Id.ToString(),
                    Text = v.Plate
                }).ToList();
            }

            return View(tripViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditSubmit(TripViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["status"] = "error";
                TempData["message"] = "Datos inválidos al editar el viaje.";
                return View("Edit", model);
            }

            var response = await _tripService.Edit(model);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "Error al actualizar viaje.";
                return View("Edit", model);
            }

            TempData["status"] = "success";
            TempData["message"] = "Viaje actualizado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _tripService.Delete(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "Error al eliminar viaje.";
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["message"] = "Viaje eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }*/
    }

}
