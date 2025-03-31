using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ScentifyAdmin.Models.Entities
{
    public class Perfume
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); // Auto-generate GUID

        [Required]
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Manufacturer { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Composition { get; set; }
        public string Ingredients { get; set; }
        public string FragranceNotes { get; set; }

		[Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
        public string TopPerfumed { get; set; }
        public string MiddlePerfumed { get; set; }
        public string BasePerfumed { get; set; }
        public string PriceInfo { get; set; }

        [Required]
        public string Status { get; set; }
        public string ImageUrl { get; set; }
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
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}