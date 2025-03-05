using Microsoft.AspNetCore.Mvc;
using ScentifyWebApp.Services;

namespace ScentifyWebApp.Controllers
{
    public class CheckoutController : Controller
	{
		private readonly CartService _cartService;
		private readonly MomoService _momoService;

		public CheckoutController(MomoService momoService
            , CartService cartService)
		{
			_cartService = cartService;
            _momoService = momoService;
        }

		public IActionResult Index()
        {
            return View();
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
    }
}
