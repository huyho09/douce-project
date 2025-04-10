using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ScentifyWebApp.DAL.DB;
using ScentifyWebApp.Libs;
using ScentifyWebApp.Models.Dtos;
using ScentifyWebApp.Models.Entities;
using ScentifyWebApp.Models.ViewModels;

namespace ScentifyWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var homeView = await RetrieveHomeViewData();

            return View(homeView);
        }

        private async Task<HomeViewModel> RetrieveHomeViewData()
        {
            var products = await _context.Perfume.Take(6).ToListAsync();

            if (products is not { Count: > 0 })
                return new HomeViewModel();

            var mappedPerfumes = _mapper.Map<List<DtoPerfume>>(products);

            for (int i = 0; i < products.Count; i++)
            {
                var product = products[i];
                var dto = mappedPerfumes[i];

                if (!string.IsNullOrWhiteSpace(product.Ingredients))
                {
                    dto.DtoIngredients = JsonConvert.DeserializeObject<List<Ingredient>>(product.Ingredients);
                }

                if (!string.IsNullOrWhiteSpace(product.PriceInfo))
                {
                    var productSizes = JsonHelpers.ParseJson<ProductSize>(product.PriceInfo);
                    if (productSizes?.Any() == true)
                    {
                        dto.ProductSizes = productSizes;
                    }
                }

                if (!string.IsNullOrWhiteSpace(product.FragranceNotes))
                {
                    var notes = JsonHelpers.ParseJson<FragranceNote>(product.FragranceNotes)?.FirstOrDefault();
                    if (notes is not null)
                    {
                        dto.Citrus = notes.Citrus;
                        dto.Floral = notes.Floral;
                        dto.Fruity = notes.Fruity;
                        dto.Woody = notes.Woody;
                        dto.Musky = notes.Musky;
                        dto.Oriental = notes.Oriental;
                        dto.Spicy = notes.Spicy;
                        dto.Tobacco = notes.Tobacco;
                        dto.Gourmand = notes.Gourmand;
                    }
                }
            }

            return new HomeViewModel
            {
                DtoPerfumes = mappedPerfumes
            };
        }

        private List<T> GetRandomItems<T>(List<T> list, int count)
        {
            return list.OrderBy(_ => Guid.NewGuid()).Take(count).ToList();
        }
    }
}
