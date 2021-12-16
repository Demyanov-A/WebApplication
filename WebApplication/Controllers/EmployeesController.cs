using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class EmployeesController : Controller
    {
        private static readonly List<Employee> __Employees = new()
        {
            new Employee() { Id = 1, FirstName = "Иван", LastName = "Иванов", Patronymic = "Иванович", Age = 21 },
            new Employee() { Id = 2, FirstName = "Петр", LastName = "Петров", Patronymic = "Петрович", Age = 22 },
            new Employee() { Id = 3, FirstName = "Сидор", LastName = "Сидоров", Patronymic = "Сидорович", Age = 23 },
        };
        public IActionResult Index()
        {
            var result = __Employees;
            return View(result);
        }

        public IActionResult EmployeeInfo(int id)
        {
            ViewData["TestValue"] = 123;

            var employee = __Employees.FirstOrDefault(e => e.Id.Equals(id));

            if(employee is null) return NotFound();

            ViewBag.SelectedEmployee = employee;

            return View(employee);
        }

        public IActionResult Edit()
        {
            return View();
        }

        public void AddEmployee(Employee employee)
        {
            __Employees.Add(employee);
        }

        public IActionResult RemoveEmployee(Employee employee)
        {
            if(__Employees.Contains(employee))
            {
                __Employees.Remove(employee);
                return View("Index");
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}
