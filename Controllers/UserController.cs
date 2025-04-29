using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Models.ViewModels.User;
using GestorViajes.Services.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace GestorViajes.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

    //    [httppost]
    //    public async task<iactionresult> login(loginviewmodel model)
    //    {
    //        if (!modelstate.isvalid)
    //            return view(model);

    //        var user = await _userservice.getbyemailandpassword(model.email, model.password);

    //        if (user == null)
    //        {
    //            modelstate.addmodelerror(string.empty, "credenciales incorrectas");
    //            return view(model);
    //        }

    //        // autenticación con cookies
    //        var claims = new list<claim>
    //{
    //    new claim(claimtypes.name, user.name),
    //    new claim(claimtypes.email, user.email),
    //    new claim(claimtypes.role, user.role)
    //};

    //        var identity = new claimsidentity(claims, cookieauthenticationdefaults.authenticationscheme);
    //        var principal = new claimsprincipal(identity);

    //        await httpcontext.signinasync(cookieauthenticationdefaults.authenticationscheme, principal);

    //        return redirecttoaction("index", "home");
    //    }

    //    [httppost]
    //    public async task<iactionresult> logout()
    //    {
    //        await httpcontext.signoutasync(cookieauthenticationdefaults.authenticationscheme);
    //        return redirecttoaction("login", "user");
    //    }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _userService.List();
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error!.Message;
                return View(new List<UserViewModel>());
            }

            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var response = await _userService.GetById(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message ?? "No se pudo obtener el usuario";
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubmit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["status"] = "error";
                TempData["message"] = "Datos inválidos. Verifica e intenta de nuevo.";
                return View("Create", model);
            }

            var response = await _userService.Add(model);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return View("Create", model);
            }

            TempData["status"] = "success";
            TempData["message"] = "Usuario creado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var response = await _userService.GetById(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> EditSubmit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var response = await _userService.Update(model);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return View("Edit", model);
            }

            TempData["status"] = "success";
            TempData["message"] = "Usuario actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _userService.Delete(id);
            if (!response.Success)
            {
                TempData["status"] = "error";
                TempData["message"] = response.Error?.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["message"] = "Usuario eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
