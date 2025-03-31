using Microsoft.AspNetCore.Mvc.Rendering;
using ScentifyAdmin.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ScentifyAdmin.Models.Dtos
{
	public class DtoPerfume
	{
		public string Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Brand { get; set; } = string.Empty;
		public string Manufacturer { get; set; } = string.Empty;
		public string ShortDescription { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string TopPerfumed { get; set; } = string.Empty;
		public string MiddlePerfumed { get; set; } = string.Empty;
		public string BasePerfumed { get; set; } = string.Empty;

		public PerfumedNote Perfumed_Notes_DTO { get; set; }

		public string Composition { get; set; } = string.Empty;
		public string FragranceFamily { get; set; } = string.Empty; // Nhóm hương chính (VD: Floral, Woody)
		public int VolumeMl1 { get; set; }
		public int VolumeMl2 { get; set; }
		public string Ingredients { get; set; } = string.Empty;
		public List<Ingredient>? DtoIngredients { get; set; } // Nguyên liệu
		public decimal Price1 { get; set; } = 0;
		public decimal Price2 { get; set; } = 0;
		public string Currency { get; set; } = string.Empty;
		public string FragranceNotes { get; set; }

		public bool Citrus { get; set; }
		public bool Floral { get; set; }
		public bool Fruity { get; set; }
		public bool Woody { get; set; }
		public bool Musky { get; set; }
		public bool Oriental { get; set; }
		public bool Spicy { get; set; }
		public bool Tobacco { get; set; }
		public bool Gourmand { get; set; }

		public int StockQuantity { get; set; } = 0;
		public string PriceInfo { get; set; } = "";
		public List<ProductSize> ProductSizes { get; set; }
		public string Status { get; set; }
		public IList<SelectListItem> StatusList { get; set; } = new List<SelectListItem>
		{
			new SelectListItem { Value = "Available", Text = "Available" },
			new SelectListItem { Value = "Out of Stock", Text = "Out of Stock" },
			new SelectListItem { Value = "Discontinued", Text = "Discontinued" }
		};


		public string ImageUrl { get; set; } = "/"; // URL hình ảnh sản phẩm

	}
	public class PerfumedNote
	{
		public List<string> Top { get; set; }
		public List<string> Middle { get; set; }
		public List<string> Base { get; set; }
	}

}
