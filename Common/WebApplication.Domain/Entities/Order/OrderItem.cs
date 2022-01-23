using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Domain.Entities.Base;

namespace WebApplication.Domain.Entities.Order
{
    public class OrderItem : Entity
    {
        [Required] public Product Product { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")] public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Order Order { get; set; } = null!;

        [NotMapped] public decimal TotalItemsPrice => Price * Quantity;
    }
}
