using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Domain.Entities;
using WebApplication.Interfaces;
using WebApplication.Interfaces.Services;
using WebApplication.WebAPI.Clients.Base;

namespace WebApplication.WebAPI.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(HttpClient Client) : base(Client, WebAPIAddresses.Employees)
        {
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = Get<IEnumerable<Employee>>(Address);
            return employees!;
        }

        public Employee? GetById(int id)
        {
            var result = Get<Employee>($"{Address}/{id}");
            return result;
        }

        public int Add(Employee employee)
        {
            var response = Post(Address, employee);
            var added_employee = response.Content.ReadFromJsonAsync<Employee>().Result;
            if (added_employee is null)
                return -1;
            var id = added_employee.Id;
            employee.Id = id;
            return id;
        }

        public bool Edit(Employee employee)
        {
            var response = Put(Address, employee);
            var success = response.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<bool>()
                .Result;
            return success;
        }

        public bool Delete(int id)
        {
            var response = Delete($"{Address}/{id}");
            var success = response.IsSuccessStatusCode;
            return success;
        }
    }
}
