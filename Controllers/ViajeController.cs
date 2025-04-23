using GestorViajes.Models.EFCore.GestionTurnos;
using GestorViajes.Models.ViewModels.Roadtrip;
using GestorViajes.Services.Roadtrip;
using Microsoft.AspNetCore.Mvc;

namespace GestorViajes.Controllers
{
    public class ViajeController : Controller
    {
        private readonly IViajeService _viajeService;
        private readonly IViajeDatatablesService _datatables;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;

        public ViajeController(
            IViajeService viajeService,
            IViajeDatatablesService datatables,
            IHttpContextAccessor accessor,
            IMapper mapper)
        {
            _viajeService = viajeService;
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

        // Muestra el formulario para agregar un viaje (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Procesa el formulario y agrega un viaje (POST)
        [HttpPost]
        public async Task<IActionResult> CreateSubmit(CreateViajeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var mappedEntity = _mapper.Map<Viaje>(model);
            var serviceResponse = await _viajeService.Add(mappedEntity);
            if (!serviceResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = serviceResponse.Error!.Message;
                return View(model);
            }

            TempData["status"] = "success";
            TempData["mensaje"] = "El viaje se ha creado con éxito";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        // Muestra el formulario de edicion con los datos actuales del viaje.
        public async Task<IActionResult> Edit(int id)
        {
            var viajeResponse = await _viajeService.Get(v => v.Id == id);
            if (!viajeResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = viajeResponse.Error!.Message;
                return RedirectToAction(nameof(Index));
            }

            var model = _mapper.Map<ViajeViewModel>(viajeResponse.Data);
            return View(model);
        }

        [HttpPost]
        // Recibe los datos del formulario que el usuario ha introducido y los guarda en la base de datos.
        public async Task<IActionResult> EditSubmit(ViajeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = "Error al editar el viaje";
                return View(model);
            }

            var mappedEntity = _mapper.Map<Viaje>(model);
            var serviceResponse = await _viajeService.Edit(mappedEntity);
            if (!serviceResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = serviceResponse.Error!.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["mensaje"] = "Viaje editado exitosamente";
            return RedirectToAction(nameof(Index));
        }

        // Muestra la vista de confirmacion antes de eliminar el viaje (evitar borrado accidental)
        [HttpGet]
        public async Task<IActionResult> DeleteViaje(int id)
        {
            var viajeResponse = await _viajeService.Get(v => v.Id == id);
            if (!viajeResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = viajeResponse.Error!.Message;
                return RedirectToAction(nameof(Index));
            }

            var model = _mapper.Map<ViajeViewModel>(viajeResponse.Data);
            // Muestra una vista de confirmacion antes de eliminar.
            return View(model); 
        }

        // Elimina un viaje de la base de datos
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var serviceResponse = await _viajeService.Delete(id);
            if (!serviceResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = serviceResponse.Error!.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["mensaje"] = "Viaje eliminado exitosamente";
            return RedirectToAction(nameof(Index));
        }
    }

}
