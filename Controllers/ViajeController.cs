using Microsoft.AspNetCore.Mvc;

namespace GestorViajes.Controllers
{
    public class ViajeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
