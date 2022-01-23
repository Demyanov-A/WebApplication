using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Interfaces.TestAPI;

namespace WebApplication.Controllers
{
    public class WebAPIController : Controller
    {
        private readonly IValuesService _ValuesService;

        public WebAPIController(IValuesService ValuesService) => _ValuesService = ValuesService;

        public IActionResult Index()
        {
            var values = _ValuesService.GetValues();
            return View(values);
        }
    }
}
