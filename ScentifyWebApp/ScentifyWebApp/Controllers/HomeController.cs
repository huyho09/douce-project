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
            var products = await _context.Perfume.Take(6).Select(p => new DtoPerfume
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                Brand = p.Brand,
                ImageUrl = p.ImageUrl,
                TopPerfumed = p.TopPerfumed,
                MiddlePerfumed = p.MiddlePerfumed,
                BasePerfumed = p.BasePerfumed,

                // Parse JSON inline
                DtoIngredients = !string.IsNullOrWhiteSpace(p.Ingredients)
                ? JsonConvert.DeserializeObject<List<Ingredient>>(p.Ingredients)
                : new List<Ingredient>(),

                ProductSizes = !string.IsNullOrWhiteSpace(p.PriceInfo)
                ? JsonHelpers.ParseJson<ProductSize>(p.PriceInfo)
                : new List<ProductSize>(),

                // Fragrance notes object parsing
                //FragranceNoteObj = !string.IsNullOrWhiteSpace(p.FragranceNotes)
                //? JsonHelpers.ParseJson<FragranceNote>(p.FragranceNotes).FirstOrDefault()
                //: null
                FragranceNotes = p.FragranceNotes
            })
        .ToListAsync();

            return new HomeViewModel
            {
                DtoPerfumes = products
            };
        }

        private List<T> GetRandomItems<T>(List<T> list, int count)
        {
            return list.OrderBy(_ => Guid.NewGuid()).Take(count).ToList();
        }
    }
}
