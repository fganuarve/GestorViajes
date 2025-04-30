using AutoMapper;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.Vehicle;
using GestorViajes.Services.Vehicle;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> IndexVehicle()
        {
            var response = await _vehicleService.List();
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "No se pudieron cargar los vehículos.";
                return View(new List<VehicleViewModel>());
            }

            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> DetailsVehicle(long id)
        {
            var response = await _vehicleService.Get(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "Vehículo no encontrado.";
                return RedirectToAction(nameof(IndexVehicle));
            }

            return View(response.Data);
        }

        [HttpGet]
        public IActionResult CreateVehicle()
        {
            return View(new VehicleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicleSubmit(VehicleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["status"] = "error";
                TempData["message"] = "Datos inválidos. Verifica e intenta nuevamente.";
                return View("CreateVehicle", model);
            }

            // Obtener ID del usuario autenticado desde los claims de forma segura
            //uso TryParse en caso de que el claim venga vacio o mal formado
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
            {
                TempData["status"] = "error";
                TempData["message"] = "No se pudo determinar el usuario autenticado.";
                return View("CreateVehicle", model);
            }

            model.UserId = userId;

            var response = await _vehicleService.Add(model);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return View("CreateVehicle", model);
            }

            TempData["status"] = "success";
            TempData["message"] = "Vehículo creado correctamente.";
            return RedirectToAction(nameof(IndexVehicle));
        }


        [HttpGet]
        public async Task<IActionResult> EditVehicle(long id)
        {
            var response = await _vehicleService.Get(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return RedirectToAction(nameof(IndexVehicle));
            }

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> EditVehicleSubmit(VehicleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditVehicle", model);
            }

            var response = await _vehicleService.Update(model);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return View("EditVehicle", model);
            }

            TempData["status"] = "success";
            TempData["message"] = "Vehículo actualizado correctamente.";
            return RedirectToAction(nameof(IndexVehicle));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(long id)
        {
            var response = await _vehicleService.Delete(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return RedirectToAction(nameof(IndexVehicle));
            }

            TempData["status"] = "success";
            TempData["message"] = "Vehículo eliminado correctamente.";
            return RedirectToAction(nameof(IndexVehicle));
        }

        [HttpGet]
        public async Task<IActionResult> ByUser(long userId)
        {
            var response = await _vehicleService.ListByUser(userId);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "No se pudieron cargar los vehículos del usuario.";
                return RedirectToAction(nameof(IndexVehicle));
            }

            return View("IndexVehicle", response.Data);
        }
    }
}
