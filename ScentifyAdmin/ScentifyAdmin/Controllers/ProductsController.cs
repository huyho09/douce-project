using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScentifyAdmin.DAL.DB;
using ScentifyAdmin.Models.Dtos;
using ScentifyAdmin.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ScentifyAdmin.Controllers
{
	[Route("products")]
	public class ProductsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public ProductsController(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<IActionResult> Index()
		{
			var products = await _context.Perfume.ToListAsync();
			var result = new List<DtoPerfume>();
			if (products != null && products.Count > 0)
			{
				result = _mapper.Map<List<DtoPerfume>>(products);
				List<Perfume> randomPerfumes = GetRandomItems(products, 3);

				// get similar 
				var mappingList = _mapper.Map<List<DtoPerfume>>(randomPerfumes);
				// Assign similar perfumes
				if (result != null && result.Any())
				{
					foreach (var item in result)
					{
						//item.SimilarPerfumes = mappingList;
						if (!string.IsNullOrEmpty(item.Ingredients))
						{
							var Ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(item.Ingredients);
							item.DtoIngredients = Ingredients;
						}
						if (!string.IsNullOrEmpty(item.Perfumed_Notes) && IsValidJson(item.Perfumed_Notes))
						{
							var PerfumedNotes = JsonConvert.DeserializeObject<PerfumedNote>(item.Perfumed_Notes);
							item.Perfumed_Notes_DTO = PerfumedNotes != null ? PerfumedNotes : new PerfumedNote();
						}
					}
				}
			}
			return View(result);
		}
		[HttpGet("Create")]
		public IActionResult Create()
		{
			var model = new DtoPerfume();
			return View("Create", model);
		}


		[HttpPost("Create")]
		public async Task<IActionResult> Create(DtoPerfume requestDTO)
		{
			if (requestDTO != null)
			{
				var request = _mapper.Map<Perfume>(requestDTO);
				_context.Perfume.Add(request);
				await _context.SaveChangesAsync();
				return RedirectToAction("index");
			}
			return RedirectToAction("Index","Product");
		}
		//[HttpPost]
		//public async Task<IActionResult> Edit(UpdateProductRequest request)
		//{
		//	if (FieldRequiredHelper.AreFieldsRequired(request))
		//	{
		//		var updateProduct = await _productService.UpdateProductAsync(request);
		//		TempData["ResultPopup"] = updateProduct.Detail;
		//		return Redirect("/Admin/ProductAdmin/Edit/" + request.Id);
		//	}

		//	// reload
		//	var loadProductFilterDataRequest = await LoadProductFilterDataRequest();
		//	if (!loadProductFilterDataRequest.IsSuccess)
		//	{
		//		TempData["ResultPopup"] = loadProductFilterDataRequest.Detail;
		//		return View();
		//	}

		//	request.ProductFilterDataRequest = loadProductFilterDataRequest.Data;
		//	return View("Edit", request);
		//}

		[HttpDelete]
		public async Task<IActionResult> Delete(string currentId)
		{
			if (!Guid.TryParse(currentId, out Guid guidId))
			{
			}
			var product = _context.Perfume.FirstOrDefault(m => m.Id == guidId);
			if(product != null)
			{
				_context.Perfume.Remove(product);
				await _context.SaveChangesAsync();
			}

			return Content("/Products");
		}

		[HttpGet("review")]
		public async Task<IActionResult> Review(DtoPerfume requestDTO)
		{
			return View(requestDTO);
		}

		private List<T> GetRandomItems<T>(List<T> list, int count)
		{
			return list.OrderBy(_ => Guid.NewGuid()).Take(count).ToList();
		}

		private bool IsValidJson(string str)
		{
			str = str.Trim();
			if ((str.StartsWith("{") && str.EndsWith("}")) || // Object
				(str.StartsWith("[") && str.EndsWith("]")))   // Array
			{
				try
				{
					JToken.Parse(str);
					return true;
				}
				catch (JsonReaderException)
				{
					return false;
				}
			}
			return false;
		}
	}
}
