using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Domain.DTO;
using WebApplication.Domain.Entities.Order;
using WebApplication.Domain.ViewModels;
using WebApplication.Interfaces;
using WebApplication.Interfaces.Services;
using WebApplication.WebAPI.Clients.Base;

namespace WebApplication.WebAPI.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(HttpClient Client) : base(Client, WebAPIAddresses.Orders)
        {
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(string UserName, CancellationToken Cancel = default)
        {
            var orders = await GetAsync<IEnumerable<OrderDTO>>($"{Address}/user/{UserName}").ConfigureAwait(false);
            return orders!.FromDTO()!;
        }

        public async Task<Order?> GetOrderByIdAsync(int Id, CancellationToken Cancel = default)
        {
            var order = await GetAsync<OrderDTO>($"{Address}/{Id}").ConfigureAwait(false);
            return order.FromDTO();
        }

        public async Task<Order> CreateOrderAsync(string UserName, CartViewModel Cart, OrderViewModel OrderModel, CancellationToken Cancel = default)
        {
            var model = new CreateOrderDTO
            {
                Items = Cart.ToDTO(),
                Order = OrderModel,
            };

            var response = await PostAsync($"{Address}/{UserName}", model).ConfigureAwait(false);
            var order = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<OrderDTO>(cancellationToken: Cancel)
                .ConfigureAwait(false);

            return order.FromDTO()!;
        }
    }
}
