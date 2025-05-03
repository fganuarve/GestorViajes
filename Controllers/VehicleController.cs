using AutoMapper;
using GestorViajes.Models.ViewModels.Vehicle;
using GestorViajes.Services.User;
using GestorViajes.Services.Users;
using GestorViajes.Services.Vehicle;
using GestorViajes.Services.Vehicles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorViajes.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public VehicleController(
            IUserService userService,
            IVehicleService vehicleService,
            IMapper mapper
            )
        {
            _userService = userService;
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var current = await _userService.CurrentUser();
            var response = await _vehicleService.List(x => x.UserId == current.Id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error!.Message;
                return View(new List<VehicleViewModel>());
            }

            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var response = await _vehicleService.Get(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error!.Message;
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleViewModel input)
        {
            var response = await _vehicleService.Add(input);

            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error!.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["message"] = "Vehículo registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {

            var response = await _vehicleService.Get(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error!.Message;
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VehicleViewModel input)
        {
            var response = await _vehicleService.Update(input);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error!.Message;
                return RedirectToAction(nameof(Index), new { id = input.Id });
            }

            TempData["status"] = "success";
            TempData["message"] = "Vehículo actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }


        //Borrado que deberia hacer solo un admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _vehicleService.Delete(id);

            TempData["status"] = response.Success ? "success" : "danger";
            TempData["message"] = response.Success
                ? "Vehículo eliminado correctamente."
                : $"Ocurrió un error al eliminar el vehículo. {response.Error?.Message}";

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleVehicle(long id)
        {
            var response = await _vehicleService.Toggle(id);

            TempData["status"] = response.Success ? "success" : "danger";
            TempData["message"] = response.Success
                ? "Vehículo actualizado correctamente."
                : $"Ocurrió un error al actualizar el vehículo. {response.Error?.Message}";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleDetails(int id)
        {
            var response = await _vehicleService.Get(id);
            if (!response.Success)
            {
                return Json(new { success = false, message = response.Error!.Message });
            }
            return Json(new { success = true, data = response.Data });
        }
    }
}
