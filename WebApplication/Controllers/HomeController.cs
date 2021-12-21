﻿using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        //return Content("Данные из первого контроллера!");
        return View();
    }

    public string ConfiguredAction(string id, string Value1)
    {
        return $"Hello World {id} - {Value1}";
    }

    public void Throw(string Message) => throw new ApplicationException(Message);
}

