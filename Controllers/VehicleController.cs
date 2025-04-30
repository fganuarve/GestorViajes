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
            return View(new VehicleViewModel
            {    
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVehicleSubmit(VehicleViewModel input)
        {            
            var response = await _vehicleService.Add(input);

            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = "Error al registrar el vehículo.";
                return View("CreateVehicle", input);
            }

            TempData["status"] = "success";
            TempData["message"] = "Vehículo registrado correctamente.";
            return RedirectToAction("IndexVehicle");
        }

        [HttpGet]
        public async Task<IActionResult> EditVehicle(long id)
        {
            // Obtener el ID del usuario autenticado
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
            {
                TempData["status"] = "danger";
                TempData["message"] = "No se pudo determinar el usuario autenticado.";
                return RedirectToAction(nameof(IndexVehicle));
            }

            var response = await _vehicleService.Get(id);
            if (!response.Success || response.Data == null)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "Vehículo no encontrado.";
                return RedirectToAction(nameof(IndexVehicle));
            }

            // Verificar que el vehículo pertenece al usuario autenticado
            if (response.Data.UserId != userId)
            {
                TempData["status"] = "danger";
                TempData["message"] = "No tienes permiso para editar este vehículo.";
                return RedirectToAction(nameof(IndexVehicle));
            }

            return View(response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVehicleSubmit(VehicleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditVehicle", model);
            }

            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
            {
                TempData["status"] = "error";
                TempData["message"] = "No se pudo determinar el usuario autenticado.";
                return View("EditVehicle", model);
            }

            if (model.Id == null)
            {
                TempData["status"] = "error";
                TempData["message"] = "ID de vehículo no válido.";
                return View("EditVehicle", model);
            }

            var vehicleResult = await _vehicleService.Get(model.Id.Value);
            if (vehicleResult.Error != null || vehicleResult.Data == null || vehicleResult.Data.UserId != userId)
            {
                TempData["status"] = "error";
                TempData["message"] = "No tienes permiso para editar este vehículo.";
                return RedirectToAction(nameof(IndexVehicle));
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


        //Borrado que deberia hacer solo un admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVehicle(long id)
        {
            //// Validar que el usuario es administrador
            //var roleClaim = User.FindFirst("Rol")?.Value;
            //if (string.IsNullOrEmpty(roleClaim) || roleClaim.ToLower() != "admin")
            //{
            //    TempData["status"] = "danger";
            //    TempData["message"] = "No tienes permiso para realizar esta acción.";
            //    return RedirectToAction(nameof(IndexVehicle));
            //}

            // Verificar que el vehículo exista antes de intentar eliminarlo
            var vehicleResult = await _vehicleService.Get(id);
            if (!vehicleResult.Success || vehicleResult.Data == null)
            {
                TempData["status"] = "danger";
                TempData["message"] = "Vehículo no encontrado.";
                return RedirectToAction(nameof(IndexVehicle));
            }

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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeactivateVehicle(long id)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
            {
                TempData["message"] = "No se pudo determinar el usuario autenticado.";
                TempData["status"] = "danger";
                return RedirectToAction("MyVehicles");
            }

            // Validar que el vehículo pertenece al usuario autenticado
            var vehicleResult = await _vehicleService.Get(id);
            if (vehicleResult.Error != null || vehicleResult.Data == null || vehicleResult.Data.UserId != userId)
            {
                TempData["message"] = "No tienes permiso para modificar este vehículo.";
                TempData["status"] = "danger";
                return RedirectToAction("MyVehicles");
            }

            var response = await _vehicleService.DeactivateVehicle(id);

            if (response.Error != null)
            {
                TempData["message"] = "Ocurrió un error al desactivar el vehículo.";
                TempData["status"] = "danger";
            }
            else
            {
                TempData["message"] = "Vehículo desactivado correctamente.";
                TempData["status"] = "success";
            }
            return RedirectToAction("IndexVehicle");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReactivateVehicle(long id)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
            {
                TempData["message"] = "No se pudo determinar el usuario autenticado.";
                TempData["status"] = "danger";
                return RedirectToAction("MyVehicles");
            }

            // Validar que el vehículo pertenece al usuario autenticado
            var vehicleResult = await _vehicleService.Get(id);
            if (vehicleResult.Error != null || vehicleResult.Data == null || vehicleResult.Data.UserId != userId)
            {
                TempData["message"] = "No tienes permiso para modificar este vehículo.";
                TempData["status"] = "danger";
                return RedirectToAction("IndexVehicle");
            }

            var response = await _vehicleService.ReactivateVehicle(id);

            if (response.Error != null)
            {
                TempData["message"] = "Ocurrió un error al reactivar el vehículo.";
                TempData["status"] = "danger";
            }
            else
            {
                TempData["message"] = "Vehículo reactivado correctamente.";
                TempData["status"] = "success";
            }
            return RedirectToAction("IndexVehicle");
        }
        
    }
}
