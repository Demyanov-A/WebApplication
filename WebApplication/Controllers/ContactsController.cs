using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
