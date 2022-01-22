using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApplication.Domain.Entities.Identity;

namespace WebApplication.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrators)]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
