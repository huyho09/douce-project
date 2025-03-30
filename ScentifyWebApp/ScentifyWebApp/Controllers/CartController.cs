using Microsoft.AspNetCore.Mvc;
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
            return PartialView(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(string productId, int quantity)
        {
            try
            {
                // Add to cart logic
                var addedPerfume = await _cartService.AddToCart(productId, quantity);
                if (addedPerfume == null)
                {
                    return Json(new { status = 400, message = "Adding the perfume to the cart failed." });
                }

                return Json(new { status = 200, message = $"{addedPerfume.Name} added {quantity} to cart successfully 🎉" });
            }
            catch (Exception ex)
            {
                // Log the exception (replace with your logging system)
                Console.WriteLine($"Error: {ex.Message}");

                return Json(new { status = 500, message = "An error occurred while adding the perfume to the cart." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(string productId, int quantity)
        {
            try
            {
                // Add to cart logic
                var updatedPerfume = await _cartService.UpdateQuatity(productId, quantity);
                if (updatedPerfume == null)
                {
                    return Json(new { status = 400, message = "Updating the perfume to the cart failed." });
                }

                return Json(new { status = 200, message = $"{updatedPerfume.Name} updated {quantity} to cart successfully 🎉" });
            }
            catch (Exception ex)
            {
                // Log the exception (replace with your logging system)
                Console.WriteLine($"Error: {ex.Message}");

                return Json(new { status = 500, message = "An error occurred while updating the perfume to the cart." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(string productId)
        {
            try
            {
                if (string.IsNullOrEmpty(productId))
                {
                    return Json(new { status = 400, message = "Invalid perfume ID." });
                }

                // Remove cart logic
                var removeCart = await _cartService.RemoveFromCart(productId);
                if (removeCart == null)
                {
                    return Json(new { status = 400, message = "Removing the perfume to the cart failed." });
                }

                return Json(new { status = 200, message = $"{removeCart.Name} removed to cart successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception (replace with your logging system)
                Console.WriteLine($"Error: {ex.Message}");

                return Json(new { status = 500, message = "An error occurred while removing the perfume to the cart." });
            }
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            try
            {
                _cartService.ClearCart();
                return Json(new { status = 200, message = "Clear all cart successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception (replace with your logging system)
                Console.WriteLine($"Error: {ex.Message}");

                return Json(new { status = 500, message = "An error occurred while removing the perfume to the cart." });
            }
        }
    }
}
