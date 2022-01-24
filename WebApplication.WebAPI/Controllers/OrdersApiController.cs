using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Domain.DTO;
using WebApplication.Interfaces.Services;

namespace WebApplication.WebAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrderService _OrderService;

        public OrdersApiController(IOrderService OrderService) => _OrderService = OrderService;

        [HttpGet("user/{UserName}")]
        public async Task<IActionResult> GetUserOrders(string UserName)
        {
            var orders = await _OrderService.GetUserOrdersAsync(UserName);
            return Ok(orders.ToDTO());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOrderById(int Id)
        {
            var order = await _OrderService.GetOrderByIdAsync(Id);
            if (order is null)
                return NotFound();

            return Ok(order.ToDTO());
        }

        [HttpPost("{UserName}")]
        public async Task<IActionResult> CreateOrder(string UserName, [FromBody] CreateOrderDTO Model)
        {
            var order = await _OrderService.CreateOrderAsync(UserName, Model.Items.ToCartView(), Model.Order);
            return CreatedAtAction(nameof(GetOrderById), new { order.Id }, order.ToDTO());
        }
    }
}
