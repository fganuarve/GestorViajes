/*using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.Vehicle;
using GestorViajes.Services.Vehicle;
using Microsoft.AspNetCore.Mvc;

namespace GestorViajes.Controllers
{
    public class VehiculoController : Controller
    {
        private readonly IVehiculoService _vehiculoService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;

        public VehiculoController(
            IVehiculoService vehiculoService,
            IVehiculoDatatablesService datatables,
            IHttpContextAccessor accessor,
            IMapper mapper)
        {
            _vehiculoService = vehiculoService;
            _datatables = datatables;
            _accessor = accessor;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetData()
        {
            var response = await _datatables.List(Request, "");

            return Ok(response);
        }

        // Muestra el formulario para agregar un vehiculo (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Procesa el formulario y agrega un vehiculo (POST)
        [HttpPost]
        public async Task<IActionResult> CreateSubmit(CreateVehiculoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var mappedEntity = _mapper.Map<Vehiculo>(model);
            var serviceResponse = await _vehiculoService.Add(mappedEntity);
            if (!serviceResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = serviceResponse.Error!.Message;
                return View(model);
            }

            TempData["status"] = "success";
            TempData["mensaje"] = "El vehículo se ha creado con éxito";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        // Muestra el formulario de edicion con los datos actuales del vehiculo.
        public async Task<IActionResult> Edit(int id)
        {
            var vehiculoResponse = await _vehiculoService.Get(v => v.Id == id);
            if (!vehiculoResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = vehiculoResponse.Error!.Message;
                return RedirectToAction(nameof(Index));
            }

            var model = _mapper.Map<VehiculoViewModel>(vehiculoResponse.Data);
            return View(model);
        }

        [HttpPost]
        // Recibe los datos del formulario que el usuario ha introducido y los guarda en la base de datos.
        public async Task<IActionResult> EditSubmit(VehiculoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = "Error al editar el vehículo";
                return View(model);
            }

            var mappedEntity = _mapper.Map<Vehiculo>(model);
            var serviceResponse = await _vehiculoService.Edit(mappedEntity);
            if (!serviceResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = serviceResponse.Error!.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["mensaje"] = "Vehículo editado exitosamente";
            return RedirectToAction(nameof(Index));
        }

        // Muestra la vista de confirmacion antes de eliminar el vehiculo (evitar borrado accidental)
        [HttpGet]
        public async Task<IActionResult> DeleteVehiculo(int id)
        {
            var vehiculoResponse = await _vehiculoService.Get(v => v.Id == id);
            if (!vehiculoResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = vehiculoResponse.Error!.Message;
                return RedirectToAction(nameof(Index));
            }

            var model = _mapper.Map<VehiculoViewModel>(vehiculoResponse.Data);
            // Muestra una vista de confirmacion antes de eliminar.
            return View(model); 
        }

        // Elimina un vehiculo de la base de datos
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var serviceResponse = await _vehiculoService.Delete(id);
            if (!serviceResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = serviceResponse.Error!.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["mensaje"] = "Vehículo eliminado exitosamente";
            return RedirectToAction(nameof(Index));
        }
    }

}*/
