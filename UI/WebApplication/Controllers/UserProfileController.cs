using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Domain.ViewModels;
using WebApplication.Interfaces.Services;

namespace WebApplication.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult Index() => View();
        public async Task<IActionResult> Orders([FromServices] IOrderService OrderService)
        {
            var orders = await OrderService.GetUserOrdersAsync(User.Identity!.Name!);

            return View(orders.Select(order => new UserOrderViewModel
            {
                Id = order.Id,
                Address = order.Address,
                Phone = order.Phone,
                Description = order.Description,
                TotalPrice = order.TotalPrice,
                Date = order.Date,
            }));
        }
    }
}
