using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cart()
        {
            return View();
        }

        public IActionResult checkout()
        {
            return View();
        }

        public IActionResult login()
        {
            return View();
        }

        public IActionResult productDetails()
        {
            return View();
        }
    }
}
