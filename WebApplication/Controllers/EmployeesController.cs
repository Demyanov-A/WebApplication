using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    //[Route("empl/[action]/{Id?}")]
    //[Route("Staff/{action=Index}/{Id?}")]
    public class EmployeesController : Controller
    {
        private ICollection<Employee> __Employees;

        public EmployeesController()
        {
            __Employees = TestData.Employees;
        }

        public IActionResult Index()
        {
            var result = __Employees;
            return View(result);
        }

        //[Route("~/employee/info-{id}")]
        public IActionResult EmployeeInfo(int id)
        {
            ViewData["TestValue"] = 123;

            var employee = __Employees.FirstOrDefault(e => e.Id.Equals(id));

            if(employee is null) return NotFound();

            ViewBag.SelectedEmployee = employee;

            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var employee = __Employees.FirstOrDefault(x => x.Id == id);

            if(employee is null) return NotFound();

            var model = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
            };
            
            return View(model);
        }

        public IActionResult Edit(EmployeeEditViewModel model)
        {
            //Обработка модели.....

            return RedirectToAction("Index");
        }

        //public IActionResult Create() => View();
        
        public IActionResult Delete(int id) => View();
            
        
    }
}
