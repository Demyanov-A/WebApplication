﻿using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Данные из первого контроллера!");
        }
    }
}