using WebApplication.Models;

namespace WebApplication.Services.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();

        Employee? GetById(int id);

        bool Edit(Employee employee);

        bool Delete(int id);

        bool Add(Employee employee);
    }
}
