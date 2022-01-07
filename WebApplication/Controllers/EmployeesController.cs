using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using WebApplication.Domain.Entities;
using WebApplication.Services.Interfaces;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    //[Route("empl/[action]/{Id?}")]
    //[Route("Staff/{action=Index}/{Id?}")]
    [Authorize]
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

        public IActionResult Edit(int? id)
        {
            if (id is null)
                return View(new EmployeeViewModel());

            var employee = _EmployeesData.GetById((int)id);

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
            if (model.LastName == "Асама" && model.Name == "Бин" && model.Patronymic == "Ладен")
                ModelState.AddModelError("", "Террористов на работу не берём!");

            if (ModelState.IsValid)
                return View(model);

            var employee = new Employee
            {
                Id = model.Id,
                Age = model.Age,
                FirstName = model.Name,
                LastName = model.LastName,
                Patronymic = model.Patronymic,
            };

            if (model.Id == 0)
                _EmployeesData.Add(employee);
            else if (!_EmployeesData.Edit(employee))
                return NotFound();

            return RedirectToAction("Index");
        }

        public IActionResult Create() => View("Edit", new EmployeeViewModel());

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
