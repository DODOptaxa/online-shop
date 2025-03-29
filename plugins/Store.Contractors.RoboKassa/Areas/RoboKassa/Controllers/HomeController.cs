using Microsoft.AspNetCore.Mvc;
using Store.Contractors.RoboKassa.Areas.RoboKassa.Models;

namespace Store.Contractors.RoboKassa.Areas.RoboKassa.Controllers
{
    [Area("RoboKassa")]
    public class HomeController : Controller
    {
        public IActionResult Index(decimal totalPrice)
        {
            TransactionViewModel model = new TransactionViewModel();
            model.TransactionNumber = totalPrice;
            return View(model);
        }
        // Robokassa/Home/Callback
        public IActionResult Callback(decimal totalPrice)
        {
            Console.WriteLine(totalPrice + " BEFORE");
            TransactionViewModel model = new TransactionViewModel();
            totalPrice /= 10000M;
            Console.WriteLine(totalPrice + " AFTER");
            model.TransactionNumber = totalPrice;
            return View(model);
        }
    }
}
