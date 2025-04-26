using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestorViajes.Models.EFCore.GestionTurnos;
using GestorViajes.Models.ViewModels.User;
using GestorViajes.Services.User;

namespace GestorViajes.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // Lista de usuarios
        public IActionResult Index()
        {
            return View();
        }
       

        // Formulario de creacion
        [HttpGet]
        public IActionResult Create()
        {
            var model = new UserViewModel
            {
                Roles = GetAvailableRoles()
            };
            return View(model);
        }

        // Procesa la creacion
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = GetAvailableRoles();
                return View(model);
            }

            var usuario = _mapper.Map<usuarios>(model);

            var serviceResponse = await _userService.Add(usuario);

            if (!serviceResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = serviceResponse.Error!.Message;
                return View(model);
            }

            TempData["status"] = "success";
            TempData["mensaje"] = "Usuario creado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        // Formulario de edicion
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var userResponse = await _userService.Get(u => u.id == id);

            if (!userResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = userResponse.Error!.Message;
                return RedirectToAction(nameof(Index));
            }

            var model = _mapper.Map<UserViewModel>(userResponse.Data);
            model.Roles = GetAvailableRoles();

            return View(model);
        }

        // Procesa la edicion
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = GetAvailableRoles();
                TempData["status"] = "error";
                TempData["mensaje"] = "Error al editar el usuario.";
                return View(model);
            }

            var usuario = _mapper.Map<usuarios>(model);

            var serviceResponse = await _userService.Edit(usuario);

            if (!serviceResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = serviceResponse.Error!.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["mensaje"] = "Usuario editado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        // Eliminar usuario
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var serviceResponse = await _userService.Delete(id);

            if (!serviceResponse.Success)
            {
                TempData["status"] = "error";
                TempData["mensaje"] = serviceResponse.Error!.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["mensaje"] = "Usuario eliminado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        // Roles disponibles
        private List<SelectListItem> GetAvailableRoles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "Admin", Text = "Administrador" },
                new SelectListItem { Value = "User", Text = "Usuario" }
            };
        }
    }
}
