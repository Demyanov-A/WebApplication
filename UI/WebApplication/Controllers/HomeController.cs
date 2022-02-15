using Microsoft.AspNetCore.Mvc;
using WebApplication.Interfaces.Services;
using WebApplication.Services.Mapping;

namespace WebApplication.Controllers;

public class HomeController : Controller
{
    public IActionResult Index([FromServices]IProductData ProductData)
    {
        var products = ProductData.GetProducts(new() { Page = 1, PageSize = 6 }).Products.OrderBy(p => p.Order).Take(6).ToView();
        ViewBag.Products = products;
        //return Content("Данные из первого контроллера!");
        return View();
    }

    public string ConfiguredAction(string id, string Value1)
    {
        return $"Hello World {id} - {Value1}";
    }

    public void Throw(string Message) => throw new ApplicationException(Message);

    public IActionResult Error404() => View();

    public IActionResult Status(string Code)
    {
        switch (Code)
        {
            default:
                return Content($"Status code - {Code}");

            case "404":
                return RedirectToAction(nameof(Error404));
        }
    }
}

