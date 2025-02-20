using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScentifyWebApp.Models;

namespace ScentifyWebApp.Controllers
{
	[Route("products")]
	public class ProductDetailController : Controller
	{
		private readonly ILogger<ProductDetailController> _logger;
		public ProductDetailController(ILogger<ProductDetailController> logger)
		{
			_logger = logger;
		}

		[HttpGet("details/{id}")]
		public IActionResult Index(int id = 1)
		{
			var product = _productDetail(id);
			return View(product);
		}

		private Product? _productDetail(int id)
		{
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "product.json");
			var result = new Product();
			if (!System.IO.File.Exists(filePath))
			{
				throw new Exception("Product data file not found.");
			}

			var jsonData = System.IO.File.ReadAllText(filePath);
			var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);
			if(products != null)
			{
				result = products.FirstOrDefault(x => x.Id == id);
			}
			return result;
		}
	}
}
