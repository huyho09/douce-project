using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ScentifyWebApp.DAL.DB;
using ScentifyWebApp.Libs;
using ScentifyWebApp.Models;
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
            if(product != null && product.ProductSizes != null && product.ProductSizes.Any())
            {
                var ProductSizeCurrent = product.ProductSizes.Where(x => x.VolumeMl == sizeMl).ToList();
                if(ProductSizeCurrent != null)
                {
                    product.ProductSizes = ProductSizeCurrent;
				}
			}
			ViewData["sizeMl"] = sizeMl;
            return View(product);
        }

        private async Task<DtoPerfume> _productDetail(Guid id)
        {
            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "product.json");
            var result = new DtoPerfume();
            //if (!System.IO.File.Exists(filePath))
            //{
            //	throw new Exception("Product data file not found.");
            //}

            //var jsonData = System.IO.File.ReadAllText(filePath);
            //var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);
            var products = await _context.Perfume.ToListAsync();
            if (products != null && products.Count > 0)
            {
                var perfume = products.FirstOrDefault(x => x.Id == id);
                if (perfume != null)
                {
                    result = _mapper.Map<DtoPerfume>(perfume);
                    List<Perfume> randomPerfumes = GetRandomItems(products, 3);

                    // get similar 
                    var mappingList = _mapper.Map<List<DtoPerfume>>(randomPerfumes);
                    result.SimilarPerfumes = mappingList;

                    // convert ingredients
                    if (!string.IsNullOrEmpty(perfume.Ingredients))
                    {
                        var dtoIngredients = JsonConvert.DeserializeObject<List<Ingredient>>(perfume.Ingredients);
                        result.DtoIngredients = dtoIngredients;
                    }

					if (!string.IsNullOrEmpty(perfume.PriceInfo))
					{
						var PriceInfoData = perfume.PriceInfo;
						var productSizes = JsonHelpers.ParseJson<ProductSize>(PriceInfoData);
						if (productSizes != null && productSizes.Any())
						{
							result.ProductSizes = productSizes;
						}
					}
				}
            }
            return result;
        }

        private List<T> GetRandomItems<T>(List<T> list, int count)
        {
            return list.OrderBy(_ => Guid.NewGuid()).Take(count).ToList();
        }
    }
}
