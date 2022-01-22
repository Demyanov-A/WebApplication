using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.ViewModels
{
    public class UserOrderViewModel
    {
        public int Id { get; set; }

        public string Address { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? Description { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}
