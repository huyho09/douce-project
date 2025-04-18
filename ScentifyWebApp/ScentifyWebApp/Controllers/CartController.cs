using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ScentifyWebApp.Services.Contracts;

namespace ScentifyWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            return PartialView(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(string productId, int quantity, int volume)
        {
            try
            {
                // Add to cart logic
                var addedPerfume = await _cartService.AddToCart(productId, quantity, volume);
                if (addedPerfume == null)
                {
                    return Json(new { status = 400, message = "Thêm vào giỏ hàng thất bại!" });
                }

                return Json(new { status = 200, message = $"Đã thêm {quantity} {addedPerfume.Name} vào giỏ hàng thành công 🎉" });
            }
            catch (Exception ex)
            {
                // Log the exception (replace with your logging system)
                Console.WriteLine($"Error: {ex.Message}");

                return Json(new { status = 500, message = "Có lỗi xảy ra không thể thêm vào giỏ hàng!" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(string productId, int quantity, int volume)
        {
            try
            {
                // Add to cart logic
                var updatedPerfume = await _cartService.UpdateQuantity(productId, quantity, volume);
                if (updatedPerfume == null)
                {
                    return Json(new { status = 400, message = "Sửa sản phẩm trong giỏ hàng thất bại!" });
                }

                return Json(new { status = 200, message = $"Đã cập nhật {quantity} sản phẩm {updatedPerfume.Name} trong giỏ hàng thành công 🎉" });
            }
            catch (Exception ex)
            {
                // Log the exception (replace with your logging system)
                Console.WriteLine($"Error: {ex.Message}");

                return Json(new { status = 500, message = "Có lỗi xảy ra không thể sửa sản phẩm trong giỏ hàng!" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(string productId, int volume)
        {
            try
            {
                if (string.IsNullOrEmpty(productId))
                {
                    return Json(new { status = 400, message = "Invalid perfume ID." });
                }

                // Remove cart logic
                var removeCart = await _cartService.RemoveFromCart(productId, volume);
                if (removeCart == null)
                {
                    return Json(new { status = 400, message = "Xóa sản phẩm khỏi giỏ hàng thất bại!" });
                }

                return Json(new { status = 200, message = $"Đã xóa sản phẩm {removeCart.Name} khỏi giỏ hàng thành công!" });
            }
            catch (Exception ex)
            {
                // Log the exception (replace with your logging system)
                Console.WriteLine($"Error: {ex.Message}");

                return Json(new { status = 500, message = "Có lỗi xảy ra không thể xóa sản phẩm khỏi giỏ hàng!" });
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
