using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScentifyWebApp.Constant;
using ScentifyWebApp.Models;
using ScentifyWebApp.Models.Entities;
using ScentifyWebApp.Models.Requests;
using ScentifyWebApp.Services;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ScentifyWebApp.Controllers
{
    [Route("Checkout")]
    public class CheckoutController : Controller
    {
        private readonly CartService _cartService;
        private readonly MomoService _momoService;
        private readonly InvoiceService _invoiceService;
        private readonly IMapper _mapper;

        public CheckoutController(MomoService momoService
            , CartService cartService,
            InvoiceService invoiceService,
            IMapper mapper)
        {
            _cartService = cartService;
            _momoService = momoService;
            _invoiceService = invoiceService;
            _mapper = mapper;
        }

        public IActionResult SaveInvoice()
        {
            var cartItems = _cartService.GetCart();
            if (cartItems != null && cartItems.Count > 0)
            {
                return View(new CheckoutRequest(cartItems));
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
        [HttpPost]
        public async Task<IActionResult> SaveInvoice(CheckoutRequest model)
        {
            try
            {
                var cart = _cartService.GetCart();
                if (!ModelState.IsValid)
                {
                    //ViewBag.OrderId = orderId;
                    //ViewBag.Amount = amount;
                    model.CartItems = cart;
                    return View(model);
                }

                if (cart != null && cart.Count > 0)
                {

                    //var customerInfo = _mapper.Map<CustomerInfo>(model);
                    //var newInvoice = CreateInvoiceModel(cart, customerInfo);
                    ////await _invoiceService.InsertInvoiceAsync(newInvoice);

                    //// reset cart
                    ////_cartService.ClearCart();

                    //// Generate VNPay payment URL
                    //var orderId = Guid.NewGuid().ToString();
                    //var amount = cart.Sum(m => m.Quantity * m.Product.Price);

                    //string vnpayPaymentUrl = GenerateVNPayPaymentUrl(orderId, amount, "Payment for invoice #" + orderId);

                    //// Directly redirect to VNPay payment URL
                    //return Redirect(vnpayPaymentUrl);

                    //return Json(new { status = 200, message = $"Payment for your cart is pending confirmation..." });

                    // TODO testing
                    decimal amount = 1000000; // Amount in VND
                    string orderId = DateTime.Now.Ticks.ToString(); // Unique Order ID
                    string orderInfo = "Thanh toan don hang thoi gian: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "113.172.172.158";

                    string url = VNPayHelper.GenerateVNPayUrl(
                        amount: 1000000,
                        orderId: "239315",
                        orderInfo: "Thanh toan don hang thoi gian: 2025-03-23 09:49:52",
                        returnUrl: "https://douce-dev.somee.com",
                        ipAddress: "113.172.172.158"
                    );

                    Console.WriteLine(url);

                    return Redirect(url);
                }
                ViewData["ErrorPay"] = "Payment for your cart failed.";
                //return Json(new { status = 400, message = "Payment for your cart failed." });
            }
            catch (Exception ex)
            {
                ViewData["ErrorPay"] = "An error occurred while payment for your cart";
                //return Json(new { status = 500, message = "An error occurred while payment for your cart" });
            }

            return View(model);
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

        private string GenerateVNPayPaymentUrl(string orderId, decimal amount, string orderInfo)
        {
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string vnp_TmnCode = "DOUSNPON";
            string vnp_HashSecret = "2VO7BNC1ZR4WZZYN8SMHFXY1Z9EN3M1I";
            string vnp_ReturnUrl = "https://true-rings-win.loca.lt/Payment/return";

            long vnpAmount = (long)(amount * 100);
            DateTime vietnamTime = DateTime.UtcNow.AddHours(7);
            string createDate = vietnamTime.ToString("yyyyMMddHHmmss");

            var vnpayParams = new Dictionary<string, string>
    {
        { "vnp_Version", "2.1.0" },
        { "vnp_Command", "pay" },
        { "vnp_TmnCode", vnp_TmnCode },
        { "vnp_Amount", vnpAmount.ToString() },
        { "vnp_CreateDate", createDate },
        { "vnp_CurrCode", "VND" },
        { "vnp_IpAddr", "127.0.0.1" },
        { "vnp_Locale", "vn" },
        { "vnp_OrderInfo", orderInfo },
        { "vnp_OrderType", "250000" },
        { "vnp_ReturnUrl", vnp_ReturnUrl },
        { "vnp_TxnRef", orderId }
    };

            // ✅ Sort parameters before hashing
            var sortedParams = vnpayParams.OrderBy(x => x.Key).ToList();

            // ✅ Concatenate without encoding
            string hashData = string.Join("&", sortedParams.Select(x => $"{x.Key}={x.Value}"));

            System.Diagnostics.Debug.WriteLine("Hash Data (before hashing): " + hashData);

            // ✅ Generate HMAC SHA-512 hash
            string secureHash = HmacSHA512(vnp_HashSecret, hashData);

            System.Diagnostics.Debug.WriteLine("Generated Secure Hash: " + secureHash);

            vnpayParams.Add("vnp_SecureHash", secureHash);

            // ✅ Encode URL after hashing
            string paymentUrl = vnp_Url + "?" + string.Join("&", vnpayParams.Select(x => $"{x.Key}={Uri.EscapeDataString(x.Value)}"));

            System.Diagnostics.Debug.WriteLine("Payment URL: " + paymentUrl);

            return paymentUrl;
        }

        private string HmacSHA512(string key, string input)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(input); // Ensure UTF-8 encoding
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
