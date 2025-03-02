using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
						item.SimilarPerfumes = mappingList;
						if (!string.IsNullOrEmpty(item.Ingredients))
						{
							var PerfumedNotes = JsonConvert.DeserializeObject<List<Ingredient>>(item.Ingredients);
							item.DtoIngredients = PerfumedNotes;
						}
					}
				}
			}
			return View(result);
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

		//[HttpDelete]
		//public async Task<IActionResult> Delete([Required] string id)
		//{
		//	var deleteProduct = await _productService.DeleteProductAsync(id);
		//	if (!deleteProduct.IsSuccess)
		//	{
		//		return Content(deleteProduct.Detail);
		//	}

		//	TempData["ResultPopup"] = deleteProduct.Detail;
		//	return Json(true);
		//	//return Redirect("/Admin/ProductAdmin");
		//}

		private List<T> GetRandomItems<T>(List<T> list, int count)
		{
			return list.OrderBy(_ => Guid.NewGuid()).Take(count).ToList();
		}
	}
}
