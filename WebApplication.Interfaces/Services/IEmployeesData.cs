using WebApplication.Domain.Entities;

namespace WebApplication.Interfaces.Services
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();

        Employee? GetById(int id);

        bool Edit(Employee employee);

        bool Delete(int id);

        int Add(Employee employee);
    }
}
