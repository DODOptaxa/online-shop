using Microsoft.AspNetCore.Mvc;

namespace Store.Contractors.RoboKassa.Areas.RoboKassa.Controllers
{
    [Area("RoboKassa")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // Robokassa/Home/Callback
        public IActionResult Callback()
        {
            return View();
        }
    }
}
