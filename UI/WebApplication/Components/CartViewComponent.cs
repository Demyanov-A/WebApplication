using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Interfaces.Services;

namespace WebApplication.Components
{
    public class CartViewComponent : ViewComponent
    {
        #region Старый вариант

        //private readonly ICartService _CartService;

        //public CartViewComponent(ICartService CartService) => _CartService = CartService;

        //public IViewComponentResult Invoke()
        //{
        //    ViewBag.Count = _CartService.GetViewModel().ItemsCount;
        //    return View();
        //} 

        #endregion

        private readonly ICartStore _CartStore;
        public CartViewComponent(ICartStore CartStore) => _CartStore = CartStore;

        public IViewComponentResult Invoke()
        {
            ViewBag.Count = _CartStore.Cart.ItemsCount;
            return View();
        }
    }
}