using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Interfaces.TestAPI
{
    public interface IValuesService
    {
        IEnumerable<string> GetValues();

        int Count();

        string? GetById(int Id);

        void Add(string Value);

        void Edit(int Id, string Value);

        bool Delete(int Id);
    }
}
