using System.ComponentModel.DataAnnotations;

namespace ScentifyWebApp.Models.Entities
{
	public class Perfume
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid(); // Auto-generate GUID

		[Required]
		public string Name { get; set; }
		public string Brand { get; set; } = string.Empty;
		public string Manufacturer { get; set; } = string.Empty;
		public string ShortDescription { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Composition { get; set; } = string.Empty;
		public string Ingredients { get; set; } = string.Empty;
		public string FragranceNotes { get; set; } = string.Empty;

		[Range(0, int.MaxValue)]
		public int StockQuantity { get; set; }
		public string TopPerfumed { get; set; } = string.Empty;
		public string MiddlePerfumed { get; set; } = string.Empty;
		public string BasePerfumed { get; set; } = string.Empty;
		public string PriceInfo { get; set; } = string.Empty;

		[Required]
		public string Status { get; set; } = "Available";
		public string ImageUrl { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	}

	public class PriceInfo
	{
		public int VolumeMl { get; set; }
		public decimal Price { get; set; }
		public string Currency { get; set; } = "VND";
	}

	public class Ingredient
	{
		public string Name { get; set; } = string.Empty;
		public string ImageUrl { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
	}
}