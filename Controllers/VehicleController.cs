using AutoMapper;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.Vehicle;
using GestorViajes.Services.Vehicle;
using Microsoft.AspNetCore.Mvc;

namespace GestorViajes.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _vehicleService.List();
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "No se pudieron cargar los vehículos.";
                return View(new List<VehicleViewModel>());
            }

            return View(response.Data);
            //return View("IndexVehicle", modelo);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var response = await _vehicleService.Get(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "Vehículo no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new VehicleViewModel());
            //Tambien podria ser return View("CreateVehicle", new VehicleViewModel());
            //pero de forma convencional, con  return View(new VehicleViewModel()); es suficiente
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubmit(VehicleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["status"] = "error";
                TempData["message"] = "Datos inválidos. Verifica e intenta nuevamente.";
                return View("Create", model);
            }

            var response = await _vehicleService.Add(model);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return View("Create", model);
            }

            TempData["status"] = "success";
            TempData["message"] = "Vehículo creado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var response = await _vehicleService.Get(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> EditSubmit(VehicleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var response = await _vehicleService.Update(model);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return View("Edit", model);
            }

            TempData["status"] = "success";
            TempData["message"] = "Vehículo actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _vehicleService.Delete(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["message"] = "Vehículo eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ByUser(long userId)
        {
            var response = await _vehicleService.ListByUser(userId);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "No se pudieron cargar los vehículos del usuario.";
                return RedirectToAction(nameof(Index));
            }

            return View("Index", response.Data);
        }
    }
}
