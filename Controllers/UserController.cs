/*using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestorViajes.Models.EFCore;
using GestorViajes.Models.ViewModels;
using GestorViajes.Services;
using GestorViajes.Services.Datatables.User;
using GestorViajes.Utils.TempData;

namespace GestorViajes.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserDatatablesService _datatables;
        //private readonly IAddressService _addressService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;
        public UserController(
            IUserService userService,
            IUserDatatablesService datatables,
            //IAddressService addressService,
            IHttpContextAccessor accessor,
            IMapper mapper)
        {
            _userService = userService;
            _datatables = datatables;
            //_addressService = addressService;
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


        // Muestra el formulario para agregar un usuario (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateStep1()
        {
            return PartialView("_CreateStep1", new CreateUserStep1ViewModel());
        }

        [HttpPost]
        public IActionResult CreateStep1(CreateUserStep1ViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Anti Tamper protection
            TempData.StoreModelInTempData(model); // Store dynamically
            return Json(new { success = true, nextStep = "CreateStep2" });
        }

        [HttpGet]
        public ActionResult CreateStep2()
        {
            return PartialView("_CreateStep2", new AddressViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStep2(AddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Add the address if doesnt exists in the database
            var addressResponse = await _addressService.Exists(model);
            if (!addressResponse.Success)
            {
                return View(model);
            }
            if (!addressResponse.Data)
            {
                var addressSaved = await _addressService.Add(model);
                if (!addressSaved.Success)
                {
                    return View(model);
                }
            }

            TempData.StoreModelInTempData(model);
            return Json(new { success = true, nextStep = "CreateStep3" });
        }

        [HttpGet]
        public async Task<IActionResult> CreateStep3()
        {
            var roles = await _userService.ListRoles();
            if (!roles.Success)
            {
                TempData.Clear();
                return RedirectToAction(nameof(Index));
            }
            var model = new CreateUserStep3ViewModel();
            model.Roles = roles.Data!;
            return PartialView("_CreateStep3", model);
        }

        // Procesa el formulario y agrega un usuario 
        [HttpPost]
        public async Task<IActionResult> CreateSubmit(CreateUserStep3ViewModel model)
        {   //Validación del modelo
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateStep3", model);
            }

            // Recover all the steps
            var userModel = TempData.RetrieveModelFromTempData<CreateUserViewModel>();
            userModel.SelectedRole = model.SelectedRole;
            var mapedEntity = _mapper.Map<CreateUserViewModel, User>(userModel);
            //Llamada al servicio
            var serviceResponse = await _userService.Add(mapedEntity);
            if (!serviceResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = serviceResponse.Error!.Message;
                return View(model);
            }

            //Redirección a la pantalla de inicio del controlador
            TempData["status"] = "success";
            TempData["mensaje"] = "El usuario se ha creado con éxito";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        //Mostrar el formulario de edición con los datos actuales del usuario.
        public async Task<IActionResult> Edit(string id)
        {
            var userResponse = await _userService.Get(u => u.Id == id);
            if (!userResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = userResponse.Error!.Message;
                return RedirectToAction(nameof(Index));
            }

            var model = _mapper.Map<UserViewModel>(userResponse.Data);
            model.Roles = new List<SelectListItem>()
            {
                new SelectListItem(){ Value = "1", Text = "Admin"},
                new SelectListItem(){ Value = "2", Text = "Veterinarian"},
                new SelectListItem(){ Value = "3", Text = "User"},
            };
            return View(model);
        }

        [HttpPost]
        //Recibe los datos del formulario que el usuario ha introducido y los guarda en la base de datos.
        public async Task<IActionResult> EditSubmit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = "Error al editar el usuario";
                return View(model);
            }

            var mappedEntity = _mapper.Map<User>(model);
            var serviceResponse = await _userService.Edit(mappedEntity);
            if (!serviceResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = serviceResponse.Error!.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["status"] = "success";
            TempData["mensaje"] = "Usuario editado exitosamente";
            return RedirectToAction(nameof(Index));
        }

        // TODO: Darle otra vuelta al delete
        // Llamarlo por ajax? Mostrar una vista con confirmacion? Por decidir
        //Muestra la vista de confirmación antes de eliminar el usuario (evitar borrado accidental)
        public async Task<IActionResult> DeleteUser(string id)
        {
            var serviceResponse = await _userService.Delete(id);
            if (!serviceResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = serviceResponse.Error!.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["status"] = "success";
            TempData["mensaje"] = "Usuario eliminado exitosamente";
            return RedirectToAction(nameof(Index));
        }
    }
}*/
