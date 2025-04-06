namespace ScentifyWebApp.Services.Contracts
{
    public interface IMomoService
    {
        public Task<string> CheckPaymentStatus(string orderId, string requestId);

        public Task<string> CreateCaptureWalletPayment(string orderId, long amount, string redirectUrl);
    }
}
