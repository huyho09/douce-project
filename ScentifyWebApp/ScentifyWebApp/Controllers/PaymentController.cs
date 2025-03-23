using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ScentifyWebApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly string vnp_HashSecret = "2VO7BNC1ZR4WZZYN8SMHFXY1Z9EN3M1I"; // Replace with your VNPay HashSecret

        [HttpGet]
        public IActionResult Return()
        {
            // Get all query parameters from VNPay
            var vnpayData = Request.Query;
            var responseParams = new Dictionary<string, string>();
            foreach (var key in vnpayData.Keys)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    responseParams.Add(key, vnpayData[key]);
                }
            }

            // Verify the secure hash
            string vnp_SecureHash = vnpayData["vnp_SecureHash"];
            responseParams.Remove("vnp_SecureHash");
            var sortedParams = responseParams.OrderBy(x => x.Key).ToList();
            string hashData = string.Join("&", sortedParams.Select(x => $"{x.Key}={Uri.EscapeDataString(x.Value)}"));
            string computedHash = HmacSHA512(vnp_HashSecret, hashData);

            if (computedHash.Equals(vnp_SecureHash, StringComparison.OrdinalIgnoreCase))
            {
                // Hash is valid, check the response code
                string responseCode = vnpayData["vnp_ResponseCode"];
                if (responseCode == "00")
                {
                    // Payment successful
                    string transactionNo = vnpayData["vnp_TransactionNo"];
                    string amount = vnpayData["vnp_Amount"];
                    string orderId = vnpayData["vnp_TxnRef"];
                    // Update your invoice status in the database if needed
                    // Example: await _invoiceService.UpdateInvoiceStatus(orderId, "Paid");
                    return View("PaymentSuccess", new { OrderId = orderId, TransactionNo = transactionNo, Amount = decimal.Parse(amount) / 100 });
                }
                else
                {
                    // Payment failed
                    return View("PaymentFailed", new { ErrorCode = responseCode });
                }
            }

            // Invalid hash
            return View("PaymentFailed", new { ErrorCode = "Invalid hash" });
        }

        private string HmacSHA512(string key, string input)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
