using Microsoft.AspNetCore.Mvc;
using ScentifyWebApp.Models;
using ScentifyWebApp.Services;

namespace ScentifyWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            return PartialView("_CartPartial", cart);
        }

        [HttpPost]
        public IActionResult AddToCart(Product product, int quantity)
        {
            _cartService.AddToCart(product, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return RedirectToAction("Index");
        }
    }
}
