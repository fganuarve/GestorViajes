using Microsoft.AspNetCore.Mvc;

namespace GestorViajes.Controllers
{
    public class VehiculoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
