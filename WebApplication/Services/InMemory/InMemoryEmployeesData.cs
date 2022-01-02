using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly ILogger<InMemoryEmployeesData> _Logger;
        private readonly ICollection<Employee> _Employees;

        private int _MaxFreeId;
        public InMemoryEmployeesData(ILogger<InMemoryEmployeesData> logger) //ILogger - обязательно интерфейс, а не класс!!! <InMemoryEmployeesData> - это название заголовков в журнале
        {
            _Logger = logger;
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
            {
                _Logger.LogWarning("Попытка удаления отсутствующего сотрудника с Id:{0}!", employee.Id);
                return false;
            }

            _Employees.Remove(employee);

            _Logger.LogWarning("Был удален сотрудник с Id:{0}!", employee.Id);

            return true;
        }

        public bool Edit(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if(_Employees.Contains(employee))
                return true;

            var db_employee = GetById(employee.Id);
            if (db_employee is null)
            {
                _Logger.LogWarning("Попытка редактирования отсутствующего сотрудника с Id:{0}!", employee.Id);
                return false;
            }
                

            db_employee.Age = employee.Age;
            db_employee.FirstName = employee.FirstName;
            db_employee.LastName = employee.LastName;
            db_employee.Patronymic = employee.Patronymic;

            _Logger.LogWarning("Информация о сотруднике с Id:{0} была изменена!", employee.Id);

            return true;
        }

        public IEnumerable<Employee> GetAll() => _Employees;
        
        public Employee? GetById(int id) => _Employees.FirstOrDefault(e => e.Id == id);
    }
}
