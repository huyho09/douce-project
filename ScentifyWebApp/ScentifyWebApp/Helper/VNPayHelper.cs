using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ScentifyWebApp.Helper
{
    public class VNPayHelper
    {
        private const string vnp_HashSecret = "8JU07KSPP64UNO1BTZ01M7SLUF4LVGQ2"; // 🔑 Replace with your actual secret key

        public static string GenerateVNPayUrl(decimal amount, string orderId, string orderInfo, string returnUrl, string ipAddress)
        {
            DateTime now = DateTime.UtcNow.AddHours(7); // Convert to Vietnam time
            string vnp_CreateDate = now.ToString("yyyyMMddHHmmss");
            string vnp_ExpireDate = now.AddMinutes(15).ToString("yyyyMMddHHmmss"); // Ensure valid expiration

            var vnp_Params = new SortedDictionary<string, string>
        {
            { "vnp_Version", "2.1.1" },
            { "vnp_Command", "pay" },
            { "vnp_TmnCode", "TJZV0QSX" }, // 🔹 Use correct TmnCode
            { "vnp_Amount", ((int)(amount * 100)).ToString() },
            { "vnp_CurrCode", "VND" },
            { "vnp_TxnRef", orderId },
            { "vnp_OrderInfo", orderInfo },
            { "vnp_OrderType", "topup" },
            { "vnp_Locale", "vn" },
            { "vnp_ReturnUrl", returnUrl },
            { "vnp_IpAddr", ipAddress },
            { "vnp_CreateDate", vnp_CreateDate },
            { "vnp_ExpireDate", vnp_ExpireDate }
        };

            string queryString = string.Join("&", vnp_Params.Select(x => $"{x.Key}={HttpUtility.UrlEncode(x.Value)}"));
            string secureHash = ComputeHmacSHA512(vnp_HashSecret, queryString);

            return $"https://sandbox.vnpayment.vn/paymentv2/vpcpay.html?{queryString}&vnp_SecureHash={secureHash}";
        }

        private static string ComputeHmacSHA512(string key, string data)
        {
            using (HMACSHA512 hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
