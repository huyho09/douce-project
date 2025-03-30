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

		public string Manufacturer { get; set; } = string.Empty; // Nhà sản xuất (có thể khác thương hiệu)

		public string ShortDescription { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;
		public string TopPerfumed { get; set; } = string.Empty;
		public string MiddlePerfumed { get; set; } = string.Empty;
		public string BasePerfumed { get; set; } = string.Empty;

		public PerfumedNote Perfumed_Notes_DTO { get; set; }

		public string Composition { get; set; } = string.Empty;

		public string FragranceFamily { get; set; } = string.Empty; // Nhóm hương chính (VD: Floral, Woody)

		public int VolumeMl1 { get; set; }// Dung tích chai (VD: 30ml, 50ml, 100ml)
		public int VolumeMl2 { get; set; }

		public string Ingredients { get; set; } = string.Empty;
		public List<Ingredient>? DtoIngredients { get; set; } // Nguyên liệu

		public decimal Price1 { get; set; } = 0; // Giá bán (VNĐ hoặc USD)
		public decimal Price2 { get; set; } = 0;

		public string UnitPrice { get; set; } = string.Empty; // e.g., "€275.00 per 100 ml"

		public string Currency { get; set; } = string.Empty; // e.g., "€"

		public int StockQuantity { get; set; } = 0; // Số lượng tồn kho
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
