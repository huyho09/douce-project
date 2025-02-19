using Microsoft.AspNetCore.Mvc;

namespace ScentifyWebApp.Controllers
{
	public class ProductDetailController : Controller
	{
		private readonly ILogger<ProductDetailController> _logger;
		public ProductDetailController(ILogger<ProductDetailController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index(int id = 0)
		{
			return View();
		}
	}
}
