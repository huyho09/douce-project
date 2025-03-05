using Azure;
using Microsoft.AspNetCore.Mvc;
using ScentifyWebApp.Constant;
using ScentifyWebApp.Models;
using ScentifyWebApp.Models.Entities;
using ScentifyWebApp.Services;
using System.Text.Json;

namespace ScentifyWebApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly CartService _cartService;
        private readonly MomoService _momoService;
        private readonly InvoiceService _invoiceService;

        public CheckoutController(MomoService momoService
            , CartService cartService,
            InvoiceService invoiceService)
        {
            _cartService = cartService;
            _momoService = momoService;
            _invoiceService = invoiceService;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCart();
            if (cartItems != null && cartItems.Count > 0)
            {
                return View(cartItems);
            }
            return Redirect("home");
        }

        // Endpoint to initiate a payment
        [HttpPost("pay")]
        public async Task<IActionResult> Pay(string orderId, long amount)
        {
            try
            {
                var response = await _momoService.CreateCaptureWalletPayment(orderId, amount, "https://localhost:7159/Checkout");
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // Endpoint to initiate a payment
        [HttpPost("save-invoice")]
        public async Task<IActionResult> SaveInvoice([FromForm] CustomerInfo customerInfo)
        {
            try
            {
                var cart = _cartService.GetCart();
                if (cart != null && cart.Count > 0)
                {
                    var newInvoice = CreateInvoiceModel(cart, customerInfo);
                    await _invoiceService.InsertInvoiceAsync(newInvoice);

                    // reset cart
                    _cartService.ClearCart();

                    return Json(new { status = 200, message = $"Payment for your cart is pending confirmation..." });
                }
                return Json(new { status = 400, message = "Payment for your cart failed." });
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = "An error occurred while payment for your cart" });
            }
        }

        private Invoice CreateInvoiceModel(List<CartItem> cartItems, CustomerInfo customerInfo)
        {
            return new Invoice()
            {
                CustomerInfo = JsonSerializer.Serialize(customerInfo),
                Discount = 0,
                InvoiceItems = JsonSerializer.Serialize(cartItems),
                OtherCost = 0,
                Noted = "",
                PaymentDate = DateTime.UtcNow.ToString("yyyy-MM-dd hh-mm"),
                PaymentMethod = "Cash",
                Shipping = 0,
                Status = InvoiceStatus.PENDING
            };
        }
    }
}
