using WebApplication.Models;

namespace WebApplication.Data
{
    public class TestData
    {
        public static List<Employee> Employees { get; } = new()
        {
            new Employee() { Id = 1, FirstName = "Иван", LastName = "Иванов", Patronymic = "Иванович", Age = 21 },
            new Employee() { Id = 2, FirstName = "Петр", LastName = "Петров", Patronymic = "Петрович", Age = 22 },
            new Employee() { Id = 3, FirstName = "Сидор", LastName = "Сидоров", Patronymic = "Сидорович", Age = 23 },
        };
    }
}
