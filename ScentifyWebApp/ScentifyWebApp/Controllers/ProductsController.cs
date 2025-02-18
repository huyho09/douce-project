using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScentifyWebApp.Models;

namespace ScentifyWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private List<Product>? _products
        {
            get
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "product.json");

                if (!System.IO.File.Exists(filePath))
                {
                    throw new Exception("Product data file not found.");
                }

                var jsonData = System.IO.File.ReadAllText(filePath);
                var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);
                return products;
            }
        }

        public IActionResult Index()
        {
            return View(_products?.Take(6));
        }

        [HttpPost]
        public IActionResult LoadMoreProducts([FromBody] PaginationRequest request)
        {
            if (_products?.Count > 0)
            {
                var pagedProducts = _products.Skip((request.Page - 1) * request.Size).Take(request.Size).ToList();
                return Json(pagedProducts);
            }
            return Json(null);
        }
    }
}
