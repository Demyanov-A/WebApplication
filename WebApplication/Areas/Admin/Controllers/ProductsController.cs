using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Domain.Entities.Identity;
using WebApplication.Services.Interfaces;

namespace WebApplication.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrators)]
    public class ProductsController : Controller
    {
        private readonly IProductData _ProductData;
        private readonly ILogger<ProductsController> _Logger;

        public ProductsController(IProductData ProductData, ILogger<ProductsController> Logger)
        {
            _ProductData = ProductData;
            _Logger = Logger;
        }

        public IActionResult Index()
        {
            var products = _ProductData.GetProducts();
            return View(products);
        }
    }
}
