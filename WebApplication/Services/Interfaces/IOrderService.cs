using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Domain.Entities.Order;
using WebApplication.ViewModels;

namespace WebApplication.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrdersAsync(string UserName, CancellationToken Cancel = default);

        Task<Order?> GetOrderByIdAsync(int Id, CancellationToken Cancel = default);

        Task<Order> CreateOrderAsync(string UserName, CartViewModel Cart, OrderViewModel OrderModel, CancellationToken Cancel = default);
    }
}
