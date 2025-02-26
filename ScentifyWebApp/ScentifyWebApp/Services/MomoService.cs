using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ScentifyWebApp.Services
{
    public class MomoService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public MomoService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> CheckPaymentStatus(string orderId, string requestId)
        {
            var partnerCode = _config["Momo:PartnerCode"];
            var accessKey = _config["Momo:AccessKey"];
            var secretKey = _config["Momo:SecretKey"];
            var endpoint = "https://test-payment.momo.vn/v2/gateway/api/query";

            var rawSignature = $"accessKey={accessKey}&orderId={orderId}&partnerCode={partnerCode}&requestId={requestId}";
            var signature = ComputeHmacSha256(rawSignature, secretKey);

            var requestBody = new
            {
                partnerCode,
                accessKey,
                requestId,
                orderId,
                signature
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, jsonContent);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateCaptureWalletPayment(string orderId, long amount, string redirectUrl)
        {
            try
            {
                // Load MoMo credentials from configuration
                var partnerCode = _config["Momo:PartnerCode"];  // e.g., "MOMO"
                var accessKey = _config["Momo:AccessKey"];        // e.g., "F8BBA842ECF85"
                var secretKey = _config["Momo:SecretKey"];        // your secret key

                // Generate a unique requestId and build order info
                var requestId = Guid.NewGuid().ToString();
                var orderInfo = $"Thanh toán tiền nước hoa Douce - Gloam Eau de Parfum 50L";

                // For IPN (asynchronous notification), use a publicly accessible URL
                var ipnUrl = "https://douce.somee.com/momo-callback";
                var extraData = "";
                var requestType = "captureWallet";  // Use captureWallet for direct payment (quick pay)

                // Build the raw signature string with the exact order required by MoMo:
                // Order: accessKey, amount, extraData, ipnUrl, orderId, orderInfo, partnerCode, redirectUrl, requestId, requestType
                var rawSignature = $"accessKey={accessKey}&amount={amount}&extraData={extraData}&ipnUrl={ipnUrl}&orderId={orderId}&orderInfo={orderInfo}&partnerCode={partnerCode}&redirectUrl={redirectUrl}&requestId={requestId}&requestType={requestType}";
                var signature = ComputeHmacSha256(rawSignature, secretKey);

                // Build the request body with the same parameter names
                var requestBody = new
                {
                    partnerCode,
                    accessKey,
                    requestId,
                    orderId,
                    amount,
                    orderInfo,
                    ipnUrl,
                    extraData,
                    requestType,
                    redirectUrl,
                    signature
                };

                var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                // Log the request for debugging
                Console.WriteLine($"Sending request to MoMo: {JsonSerializer.Serialize(requestBody)}");

                // Call MoMo's sandbox endpoint (use production endpoint when ready)
                using var response = await _httpClient.PostAsync("https://test-payment.momo.vn/v2/gateway/api/create", jsonContent);
                var responseString = await response.Content.ReadAsStringAsync();

                // Log MoMo response for debugging
                Console.WriteLine($"MoMo Response: {responseString}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}, {responseString}");
                    return $"Error: {response.StatusCode}";
                }

                return responseString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return "Error: Exception occurred while creating payment request.";
            }
        }

        private static string ComputeHmacSha256(string data, string secretKey)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", "").ToLower();
        }

        public async Task<string> PayWithMomoATM(string orderId, long amount, string redirectUrl)
        {
            try
            {
                // Ensure redirectUrl is not empty.
                if (string.IsNullOrWhiteSpace(redirectUrl))
                {
                    throw new ArgumentException("redirectUrl cannot be blank.", nameof(redirectUrl));
                }

                var partnerCode = _config["Momo:PartnerCode"];
                var accessKey = _config["Momo:AccessKey"];
                var secretKey = _config["Momo:SecretKey"];
                // Generate a new requestId
                var requestId = Guid.NewGuid().ToString();
                var orderInfo = $"Payment for Order {orderId}";

                // Use a publicly accessible ipnUrl (for testing, we can use Postman Echo)
                var ipnUrl = "https://postman-echo.com/post";
                var extraData = "";
                var requestType = "payWithATM";  // Using ATM payment type

                // Build the raw signature string in the exact order expected by MoMo:
                // Order: accessKey, amount, extraData, ipnUrl, orderId, orderInfo, partnerCode, redirectUrl, requestId, requestType
                var rawSignature = $"accessKey={accessKey}&amount={amount}&extraData={extraData}&ipnUrl={ipnUrl}&orderId={orderId}&orderInfo={orderInfo}&partnerCode={partnerCode}&redirectUrl={redirectUrl}&requestId={requestId}&requestType={requestType}";
                var signature = ComputeHmacSha256(rawSignature, secretKey);

                // Build the request body using the exact same parameter names and order
                var requestBody = new
                {
                    partnerCode,
                    accessKey,
                    requestId,
                    orderId,
                    amount,
                    orderInfo,
                    ipnUrl,
                    extraData,
                    requestType,
                    redirectUrl,
                    signature
                };

                var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                // Log the request for debugging
                Console.WriteLine($"Sending request to MoMo: {JsonSerializer.Serialize(requestBody)}");

                using var response = await _httpClient.PostAsync("https://test-payment.momo.vn/v2/gateway/api/create", jsonContent);
                var responseString = await response.Content.ReadAsStringAsync();

                // Log the response for debugging
                Console.WriteLine($"MoMo Response: {responseString}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}, {responseString}");
                    return $"Error: {response.StatusCode}";
                }

                return responseString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return "Error: Exception occurred while creating payment request.";
            }
        }
    }
}
