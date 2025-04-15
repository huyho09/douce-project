using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScentifyWebApp.Models.Entities
{
    [Index(nameof(Name))]
    [Index(nameof(ShortDescription))]
    public class Perfume
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); // Auto-generate GUID

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Brand { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Manufacturer { get; set; } = string.Empty;

        [MaxLength(300)]
        public string ShortDescription { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string Composition { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string Ingredients { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string FragranceNotes { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [MaxLength(100)]
        public string TopPerfumed { get; set; } = string.Empty;

        [MaxLength(100)]
        public string MiddlePerfumed { get; set; } = string.Empty;

        [MaxLength(100)]
        public string BasePerfumed { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string PriceInfo { get; set; }

        [Required]
        public string Status { get; set; } = "Available";
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    //[Owned]
    public class PriceInfo
    {
        public string ImageUrl { get; set; } = string.Empty;
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