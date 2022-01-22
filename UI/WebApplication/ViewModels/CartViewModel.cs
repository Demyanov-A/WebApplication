using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel Product, int Quantity)> Items { get; set; }

        public int ItemsCount => Items.Sum(p => p.Quantity);

        public decimal TotalPrice => Items.Sum(p => p.Product.Price * p.Quantity);
    }
}
