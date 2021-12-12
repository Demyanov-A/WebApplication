using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> __Employees = new()
        {
            new Employee() { Id = 1, FirstName = "Иван", LastName = "Иванов", Patronymic = "Иванович", Age = 21},
            new Employee() { Id = 2, FirstName = "Петр", LastName = "Петров", Patronymic = "Петрович", Age = 22 },
            new Employee() { Id = 3, FirstName = "Сидор", LastName = "Сидоров", Patronymic = "Сидорович", Age = 23 },
        };
        public IActionResult Index()
        {
            //return Content("Данные из первого контроллера!");
            return View();
        }

        public string ConfiguredAction(string id, string Value1)
        {
            return $"Hello World {id} - {Value1}";
        }

        public IActionResult Employees()
        {
            return View(__Employees);
        }
    }
}
