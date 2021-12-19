using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly ICollection<Employee> _Employees;

        private int _MaxFreeId;
        public InMemoryEmployeesData()
        {
            _Employees = TestData.Employees;
            _MaxFreeId = _Employees.DefaultIfEmpty().Max(e => e?.Id ?? 0) + 1;
        }
        public int Add(Employee employee)
        {
            if(employee is null) 
                throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee))
                return employee.Id;

            employee.Id = _MaxFreeId++;
            _Employees.Add(employee);

            return employee.Id;
        }

        public bool Delete(int id)
        {
            var employee = GetById(id);
            if (employee is null)
                return false;

            _Employees.Remove(employee);
            return true;

        }

        public bool Edit(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            return _Employees.Contains(employee);

            var db_employee = GetById(employee.Id);
            if (db_employee is null)
                return false;

            db_employee.Age = employee.Age;
            db_employee.FirstName = employee.FirstName;
            db_employee.LastName = employee.LastName;
            db_employee.Patronymic = employee.Patronymic;

            return true;
        }

        public IEnumerable<Employee> GetAll() => _Employees;
        
        public Employee? GetById(int id) => _Employees.FirstOrDefault(e => e.Id == id);
    }
}
