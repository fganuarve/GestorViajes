using System.Diagnostics;
using GestorViajes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorViajes.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Detecta si el usuario esta logueado, si no, redirige a la vista Login
        public IActionResult Index()
        {
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
        //Para generar la vista "Sobre nosotros"
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
        //Para generar vista de "Ayuda"
        [HttpGet]
        public IActionResult Help()
        {
            return View();
        }

        //Para generar vista de "Planes de suscripcion"
        [HttpGet]
        public IActionResult Plans()
        {
            return View();
        }

        //Para generar vista de "Comunity"
        [HttpGet]
        public IActionResult Comunity()
        {
            return View();
        }
    }
}
