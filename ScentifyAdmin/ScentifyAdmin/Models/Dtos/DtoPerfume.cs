using ScentifyAdmin.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ScentifyAdmin.Models.Dtos
{
    public class DtoPerfume
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string Manufacturer { get; set; } // Nhà sản xuất (có thể khác thương hiệu)

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string Perfumed_Notes { get; set; }

        public string Flacon { get; set; }

        public string Composition { get; set; }

        public string Gender { get; set; } // "Male", "Female", "Unisex"

        public string FragranceFamily { get; set; } // Nhóm hương chính (VD: Floral, Woody)

        public int VolumeMl { get; set; }// Dung tích chai (VD: 30ml, 50ml, 100ml)

		public string Ingredients { get; set; }
		public List<Ingredient>? DtoIngredients { get; set; } // Nguyên liệu

        public decimal Price { get; set; }  // Giá bán (VNĐ hoặc USD)

        public string UnitPrice { get; set; } // e.g., "€275.00 per 100 ml"

        public string Currency { get; set; } // e.g., "€"

        public int StockQuantity { get; set; } = 0; // Số lượng tồn kho

        public string Status { get; set; } // Trạng thái: "Available", "Out of Stock", "Discontinued"

        public decimal AverageRating { get; set; } // Điểm đánh giá trung bình (từ 0 đến 5 sao)

        public int ReviewCount { get; set; } // Số lượng đánh giá

        public string ImageUrl { get; set; } // URL hình ảnh sản phẩm

        public List<DtoPerfume> SimilarPerfumes { get; set; }

        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Thời gian tạo bản ghi

        //public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Thời gian cập nhật gần nhất
    }
}
