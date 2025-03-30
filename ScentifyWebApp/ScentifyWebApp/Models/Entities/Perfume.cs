using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ScentifyWebApp.Models.Entities
{
	public class Perfume
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid(); // Auto-generate GUID

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		[Required]
		[StringLength(255)]
		public string Brand { get; set; }

		[StringLength(255)]
		public string Manufacturer { get; set; } // Nhà sản xuất (có thể khác thương hiệu)

		[StringLength(255)]
		public string ShortDescription { get; set; }

		public string Description { get; set; }

		public string Perfumed_Notes { get; set; }

		public string Flacon { get; set; }

		public string Composition { get; set; }

		[Required]
		[StringLength(10)]
		public string Gender { get; set; } // "Male", "Female", "Unisex"

		[StringLength(100)]
		public string FragranceFamily { get; set; } // Nhóm hương chính (VD: Floral, Woody)

		[Required]
		public int VolumeMl { get; set; }// Dung tích chai (VD: 30ml, 50ml, 100ml)

		//[Required]
		//[StringLength(10)]
		//public string Concentration { get; set; } // Nồng độ nước hoa (EDT, EDP, Parfum)

		//[Range(1900, 2100)]
		//public int ReleaseYear { get; set; } // Năm ra mắt sản phẩm

		//[StringLength(100)]
		//public string CountryOfOrigin { get; set; } // Quốc gia sản xuất

		[Required]
		public string Ingredients { get; set; } // Nguyên liệu

		//[StringLength(50)]
		//public string FragranceType { get; set; } // Loại nước hoa (Spray, Rollerball, Solid)

		[Required]
		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }  // Giá bán (VNĐ hoặc USD)

		[StringLength(50)]
		public string UnitPrice { get; set; } // e.g., "€275.00 per 100 ml"

		[StringLength(10)]
		public string Currency { get; set; } // e.g., "€"

		[Range(0, int.MaxValue)]
		public int StockQuantity { get; set; } = 0; // Số lượng tồn kho

		[Required]
		[StringLength(20)]
		public string Status { get; set; } // Trạng thái: "Available", "Out of Stock", "Discontinued"

		[Range(0, 5)]
		public decimal AverageRating { get; set; } // Điểm đánh giá trung bình (từ 0 đến 5 sao)

		[Range(0, int.MaxValue)]
		public int ReviewCount { get; set; } // Số lượng đánh giá

		[StringLength(500)]
		public string ImageUrl { get; set; } // URL hình ảnh sản phẩm

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Thời gian tạo bản ghi

		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Thời gian cập nhật gần nhất
	}

	public class PerfumedNote
	{
		public List<string> Top { get; set; }
		public List<string> Middle { get; set; }
		public List<string> Base { get; set; }
	}

	public class Ingredient
	{
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public string Description { get; set; }
	}
}