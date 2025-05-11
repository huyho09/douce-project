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
            var p = await _context.Perfume
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == id);

            var dto = new DtoPerfume
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                Brand = p.Brand,
                TopPerfumed = p.TopPerfumed,
                MiddlePerfumed = p.MiddlePerfumed,
                BasePerfumed = p.BasePerfumed,
                FragranceNotes = p.FragranceNotes,
                Description = p.Description,
                DtoIngredients = !string.IsNullOrWhiteSpace(p.Ingredients)
                   ? JsonConvert.DeserializeObject<List<Ingredient>>(p.Ingredients)
                   : new List<Ingredient>(),
                ProductSizes = !string.IsNullOrWhiteSpace(p.PriceInfo)
                   ? JsonHelpers.ParseJson<ProductSize>(p.PriceInfo)
                   : new List<ProductSize>()
            };

            // Step 3: Fetch similar perfumes separately
            dto.SimilarPerfumes = await _context.Perfume
                .AsNoTracking()
                .Where(sp => sp.Id != p.Id)
                .Select(sp => new DtoPerfume
                {
                    Id = sp.Id.ToString(),
                    Name = sp.Name,
                    Brand = sp.Brand,
                    ShortDescription = sp.ShortDescription,
                    ProductSizes = !string.IsNullOrWhiteSpace(sp.PriceInfo)
                        ? JsonHelpers.ParseJson<ProductSize>(sp.PriceInfo)
                        : new List<ProductSize>(),
					DtoIngredients = !string.IsNullOrWhiteSpace(sp.Ingredients)
				   ? JsonConvert.DeserializeObject<List<Ingredient>>(sp.Ingredients)
				   : new List<Ingredient>()
				})
                .ToListAsync();

            //if (p == null)
            //    return new DtoPerfume(); // or handle differently (e.g., return null or throw)

            return dto;
        }


        private List<T> GetRandomItems<T>(List<T> list, int count)
        {
            return list.OrderBy(_ => Guid.NewGuid()).Take(count).ToList();
        }
    }
}
