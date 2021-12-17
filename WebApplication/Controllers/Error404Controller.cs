using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class Error404Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
