using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ScentifyWebApp.DAL.DB;
using ScentifyWebApp.Models;
using ScentifyWebApp.Models.Dtos;
using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var dtoProducts = _mapper.Map<List<DtoPerfume>>(products);
            return View(dtoProducts?.Take(6));
        }

        // TODO
		public async Task<IActionResult> FilterFragranceFamily(string fragranceFamily)
		{
			var products = await _context.Perfume.ToListAsync();
            var filter = new List<DtoPerfume>();
            if (products?.Count > 0)
            {
                //filter = products.Where(m => m.FragranceFamily.Contains(fragranceFamily))?.Take(6)?.ToList();
                filter = _mapper.Map<List<DtoPerfume>>(products);
            }

            ViewData["fragranceFamily"] = fragranceFamily;

			return View(filter);
		}

        // TODO
        public async Task<IActionResult> Search(string searchInput)
        {
            if (!string.IsNullOrEmpty(searchInput))
            {
                var products = await _context.Perfume.ToListAsync();
                var filter = new List<DtoPerfume>();
                if (products?.Count > 0)
                {
                    searchInput = searchInput.ToUpper();
                    var dtoProducts = _mapper.Map<List<DtoPerfume>>(products);

                    filter = dtoProducts.Where(m => m.Name.ToUpper().Contains(searchInput)
                                                || m.ShortDescription.ToUpper().Contains(searchInput))?.Take(6)?.ToList();
                                                //|| m.Description.ToUpper().Contains(searchInput)
                                                //|| m.Perfumed_Notes.ToUpper().Contains(searchInput)
                }

                ViewData["searchInput"] = searchInput;
                return View(filter);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> LoadMoreProducts([FromBody] PaginationRequest request)
        {
            var products = await _context.Perfume.ToListAsync();
            if (products?.Count > 0)
            {
                var dtoProducts = _mapper.Map<List<DtoPerfume>>(products);
                var pagedProducts = dtoProducts.Skip((request.Page - 1) * request.Size).Take(request.Size).ToList();
                return Json(pagedProducts);
            }
            return Json(null);
        }
    }
}
