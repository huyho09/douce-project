using Microsoft.AspNetCore.Mvc;
using ScentifyWebApp.Services;

namespace ScentifyWebApp.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly CartService _cartService;

        public CartViewComponent(CartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }
    }
}
