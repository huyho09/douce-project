using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ScentifyWebApp.DAL.DB;
using ScentifyWebApp.Models;
using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //private List<Product>? _products
        //{
        //    get
        //    {
        //        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "product.json");

        //        if (!System.IO.File.Exists(filePath))
        //        {
        //            throw new Exception("Product data file not found.");
        //        }

        //        var jsonData = System.IO.File.ReadAllText(filePath);
        //        var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);
        //        return products;
        //    }
        //}

        public async Task<IActionResult> Index()
        {
            var products = await _context.Perfume.ToListAsync();
            return View(products?.Take(6));
        }

		public async Task<IActionResult> FilterFragranceFamily(string fragranceFamily)
		{
			var products = await _context.Perfume.ToListAsync();
            var filter = new List<Perfume>();
            if (products?.Count > 0)
            {
                filter = products.Where(m => m.FragranceFamily.Contains(fragranceFamily))?.Take(6)?.ToList();
            }

            ViewData["fragranceFamily"] = fragranceFamily;

			return View(filter);
		}

		[HttpPost]
        public async Task<IActionResult> LoadMoreProducts([FromBody] PaginationRequest request)
        {
            var products = await _context.Perfume.ToListAsync();
            if (products?.Count > 0)
            {
                var pagedProducts = products.Skip((request.Page - 1) * request.Size).Take(request.Size).ToList();
                return Json(pagedProducts);
            }
            return Json(null);
        }
    }
}
