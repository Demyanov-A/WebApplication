using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication.DAL.Context;
using WebApplication.Domain.Entities;
using WebApplication.Interfaces.Services;

namespace WebApplication.Services.Services.InSQL
{
    public class SqlEmployeesData : IEmployeesData
    {
        private readonly WebApplicationDB _db;
        private readonly ILogger<SqlEmployeesData> _Logger;

        public SqlEmployeesData(WebApplicationDB db, ILogger<SqlEmployeesData> Logger)
        {
            _db = db;
            _Logger = Logger;
        }

        public IEnumerable<Employee> GetAll() => _db.Employees.AsEnumerable();

        public Employee? GetById(int id)
        {
            //return _db.Employees.FirstOrDefault(e => e.Id == id);
            return _db.Employees.Find(id);
        }

        public int Add(Employee employee)
        {
            //_db.Employees.Add(employee);

            //_db.Add(employee);

            _db.Entry(employee).State = EntityState.Added;

            _db.SaveChanges(); // только здесь employee.Id получит значение

            return employee.Id;
        }

        public bool Edit(Employee employee)
        {
            //_db.Entry(employee).State = EntityState.Modified;
            //_db.Update(employee);
            _db.Employees.Update(employee);

            return _db.SaveChanges() != 0;
        }

        public bool Delete(int id)
        {
            //var employee = GetById(id);
            var employee = _db.Employees
                .Select(e => new Employee
                    {Id = e.Id,}) // Неполная проекция - для экономии памяти и времени на передачу данных
                .FirstOrDefault(e => e.Id == id);

            if (employee is null)
                return false;

            //_db.Entry(employee).State = EntityState.Deleted;
            //_db.Remove(employee);
            _db.Employees.Remove(employee);

            _db.SaveChanges();
            return true;
        }
    }
}
