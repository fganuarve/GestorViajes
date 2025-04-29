using System.Diagnostics;
using GestorViajes.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestorViajes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Detecta si el usuario está logueado, si no, redirige a la vista Login
        public IActionResult Index()
        {
            // Excluir la vista de registro de la redirección
            if (!User.Identity.IsAuthenticated && !Request.Path.Value.Contains("AccountRegister"))
            {
                // Redirige a la vista Login del controlador Account si no está autenticado
                return RedirectToAction("Login", "Account");
            }

            // Vista principal tras el login, si el usuario está logueado o va a la página de registro
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
