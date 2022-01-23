using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Domain.ViewModels;
using WebApplication.Interfaces.Services;

namespace WebApplication.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;
        public BrandsViewComponent(IProductData ProductData) => _ProductData = ProductData;
        public IViewComponentResult Invoke()
        {
            return View(GetBrands());
        }

        private IEnumerable<BrandViewModel> GetBrands() =>
            _ProductData.GetBrands().OrderBy(b => b.Order).Select(b => new BrandViewModel
            {
                Id = b.Id,
                Name = b.Name,
            });
    }
}
