using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ScentifyWebApp.DAL.DB;
using ScentifyWebApp.Models;
using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Controllers
{
    [Route("products")]
    public class ProductDetailController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductDetailController> _logger;
        public ProductDetailController(ApplicationDbContext context,
            ILogger<ProductDetailController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            if (!Guid.TryParse(id, out Guid guidId))
            {
                throw new Exception("Invalid Id");
            }

            var product = await _productDetail(guidId);
            return View(product);
        }

        private async Task<Perfume> _productDetail(Guid id)
        {
            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "product.json");
            var result = new Perfume();
            //if (!System.IO.File.Exists(filePath))
            //{
            //	throw new Exception("Product data file not found.");
            //}

            //var jsonData = System.IO.File.ReadAllText(filePath);
            //var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);
            var products = await _context.Perfume.ToListAsync();
            if (products != null)
            {
                result = products.FirstOrDefault(x => x.Id == id);
            }
            return result;
        }
    }
}
