using WebApplication.Domain.Entities.Base;

namespace WebApplication.Domain.Entities;

public class Employee : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public int Age { get; set; }
}

