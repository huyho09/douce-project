using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ScentifyWebApp.DAL.DB;
using ScentifyWebApp.Libs;
using ScentifyWebApp.Models.Dtos;
using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Controllers
{
    [Route("products")]
    public class ProductDetailController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductDetailController> _logger;
        private readonly IMapper _mapper;

        public ProductDetailController(ApplicationDbContext context,
            ILogger<ProductDetailController> logger,
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("details/{id}/{sizeMl?}")]
        public async Task<IActionResult> Index(string id, int? sizeMl)
        {
            if (!Guid.TryParse(id, out Guid guidId))
            {
                throw new Exception("Invalid Id");
            }

            var product = await _productDetail(guidId);
            //if (product != null && product.ProductSizes != null && product.ProductSizes.Any())
            //{
            //    var ProductSizeCurrent = product.ProductSizes.Where(x => x.VolumeMl == sizeMl).ToList();
            //    if (ProductSizeCurrent != null && ProductSizeCurrent?.Count > 0)
            //    {
            //        product.ProductSizes = ProductSizeCurrent;
            //    }
            //}       
            product.CurrentSize = sizeMl ?? product.ProductSizes?.FirstOrDefault()?.VolumeMl ?? 0;
            return View(product);
        }

        private async Task<DtoPerfume> _productDetail(Guid id)
        {
            // Step 1: Fetch the perfume by ID
            var perfume = await _context.Perfume
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (perfume == null)
                return new DtoPerfume(); // or handle differently (e.g., return null or throw)

            var dtoPerfume = _mapper.Map<DtoPerfume>(perfume);

            // Step 2: Parse Ingredients if available
            if (!string.IsNullOrWhiteSpace(perfume.Ingredients))
            {
                dtoPerfume.DtoIngredients = JsonConvert.DeserializeObject<List<Ingredient>>(perfume.Ingredients);
            }

            // Step 3: Parse PriceInfo and map sizes
            if (!string.IsNullOrWhiteSpace(perfume.PriceInfo))
            {
                var sizes = JsonHelpers.ParseJson<ProductSize>(perfume.PriceInfo);
                if (sizes?.Any() == true)
                {
                    dtoPerfume.ProductSizes = sizes;
                }
            }

            // Step 4: Fetch 3 random similar perfumes (excluding current)
            var similarPerfumes = await _context.Perfume
                .AsNoTracking()
                .Where(p => p.Id != id)
                .OrderBy(p => Guid.NewGuid()) // lightweight random sort in SQL
                .Take(3)
                .ToListAsync();

            dtoPerfume.SimilarPerfumes = _mapper.Map<List<DtoPerfume>>(similarPerfumes);

            return dtoPerfume;
        }


        private List<T> GetRandomItems<T>(List<T> list, int count)
        {
            return list.OrderBy(_ => Guid.NewGuid()).Take(count).ToList();
        }
    }
}
