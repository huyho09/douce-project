using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ScentifyWebApp.DAL.DB;
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
			var homeView = new HomeViewModel();
			var products = await _context.Perfume.ToListAsync();

			if (products?.Count > 0)
			{
				var perfumes = GetRandomItems(products, 6);
				var mappingList = _mapper.Map<List<DtoPerfume>>(perfumes);

				// convert ingredients
				for (int i = 0; i < perfumes.Count; i++)
				{
					if (!string.IsNullOrEmpty(perfumes[i].Ingredients))
					{
						var dtoIngredients = JsonConvert.DeserializeObject<List<Ingredient>>(perfumes[i].Ingredients);
						mappingList[i].DtoIngredients = dtoIngredients;
					}
				}

				homeView.DtoPerfumes = mappingList;
			}
			return homeView;
		}

		private List<T> GetRandomItems<T>(List<T> list, int count)
		{
			return list.OrderBy(_ => Guid.NewGuid()).Take(count).ToList();
		}
	}
}
