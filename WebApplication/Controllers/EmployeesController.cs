using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Services.Interfaces;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    //[Route("empl/[action]/{Id?}")]
    //[Route("Staff/{action=Index}/{Id?}")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData)
        {
            _EmployeesData = EmployeesData;
        }

        public IActionResult Index()
        {
            var result = _EmployeesData.GetAll();
            return View(result);
        }

        //[Route("~/employee/info-{id}")]
        public IActionResult EmployeeInfo(int id)
        {
            ViewData["TestValue"] = 123;

            var employee = _EmployeesData.GetById(id);

            if(employee is null) return NotFound();

            ViewBag.SelectedEmployee = employee;

            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var employee = _EmployeesData.GetById(id);

            if(employee is null) 
                return NotFound();

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
            };
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            var employee = new Employee
            {
                Id = model.Id,
                Age = model.Age,
                FirstName = model.Name,
                LastName = model.LastName,
                Patronymic = model.Patronymic,
            };

            if (!_EmployeesData.Edit(employee))
                return NotFound();

            return RedirectToAction("Index");
        }

        //public IActionResult Create() => View();

        public IActionResult Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var employee = _EmployeesData.GetById(id);
            if (!_EmployeesData.Edit(employee))
                return NotFound();

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!_EmployeesData.Delete(id))
                return NotFound();

            return RedirectToAction("Index");
        }

    }
}
