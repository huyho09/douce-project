using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ScentifyWebApp.DAL.DB;
using ScentifyWebApp.Libs;
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
            var products = await _context.Perfume.Take(6).ToListAsync();
            var dtoProducts = _mapper.Map<List<DtoPerfume>>(products);
            ConvertDtoProducts(dtoProducts, products);
            return View(dtoProducts);
        }

        //public async Task<IActionResult> FilterFragranceFamily(string fragranceFamily)
        //{
        //    if (string.IsNullOrWhiteSpace(fragranceFamily))
        //        return View(new List<DtoPerfume>());

        //    var products = await _context.Perfume
        //        .Where(p => p.FragranceFamily.Contains(fragranceFamily))
        //        .Take(6)
        //        .ToListAsync();

        //    var dtoProducts = _mapper.Map<List<DtoPerfume>>(products);
        //    ConvertDtoProducts(dtoProducts, products);

        //    ViewData["fragranceFamily"] = fragranceFamily;
        //    return View(dtoProducts);
        //}

        public async Task<IActionResult> Search(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
                return RedirectToAction("Index");

            searchInput = searchInput.ToUpper();

            var products = await _context.Perfume
                .Where(p => p.Name.ToUpper().Contains(searchInput)
                         || p.ShortDescription.ToUpper().Contains(searchInput))
                .Take(6)
                .ToListAsync();

            var dtoProducts = _mapper.Map<List<DtoPerfume>>(products);
            ConvertDtoProducts(dtoProducts, products);

            ViewData["searchInput"] = searchInput;
            return View(dtoProducts);
        }

        [HttpPost]
        public async Task<IActionResult> LoadMoreProducts([FromBody] PaginationRequest request)
        {
            var products = await _context.Perfume
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size)
                .ToListAsync();

            var dtoProducts = _mapper.Map<List<DtoPerfume>>(products);
            ConvertDtoProducts(dtoProducts, products);

            return Json(dtoProducts);
        }


        private void ConvertDtoProducts(List<DtoPerfume> dtoProducts, List<Perfume> perfumes)
        {
            for (int i = 0; i < dtoProducts.Count; i++)
            {
                var perfume = perfumes[i];
                var dto = dtoProducts[i];

                if (!string.IsNullOrEmpty(dto.PriceInfo))
                {
                    var productSizes = JsonHelpers.ParseJson<ProductSize>(dto.PriceInfo);
                    if (productSizes?.Any() == true)
                    {
                        dto.ProductSizes = productSizes;
                    }
                }

                //if (!string.IsNullOrEmpty(perfume.Ingredients))
                //{
                //    dto.DtoIngredients = JsonConvert.DeserializeObject<List<Ingredient>>(perfume.Ingredients);
                //}
            }
        }
    }
}
