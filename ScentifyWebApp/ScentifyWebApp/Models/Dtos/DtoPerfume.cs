using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Models.Dtos
{
    public class DtoPerfume
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TopPerfumed { get; set; } = string.Empty;
        public string MiddlePerfumed { get; set; } = string.Empty;
        public string BasePerfumed { get; set; } = string.Empty;

        public PerfumedNote? Perfumed_Notes_DTO { get; set; }
        public List<DtoPerfume>? SimilarPerfumes { get; set; }
        public string Composition { get; set; } = "";
        public string FragranceFamily { get; set; } = ""; // Nhóm hương chính (VD: Floral, Woody)
        public string ImageUrl1 { get; set; } = "";
        public string ImageUrl2 { get; set; } = "";
        public int VolumeMl1 { get; set; }
        public int VolumeMl2 { get; set; }
        public string Ingredients { get; set; } = "";
        public List<Ingredient>? DtoIngredients { get; set; } // Nguyên liệu
        public decimal Price1 { get; set; } = 0;
        public decimal Price2 { get; set; } = 0;
        public string Currency { get; set; } = "VND";
        public string FragranceNotes { get; set; } = "";

        public bool Citrus { get; set; }
        public bool Floral { get; set; }
        public bool Fruity { get; set; }
        public bool Woody { get; set; }
        public bool Musky { get; set; }
        public bool Oriental { get; set; }
        public bool Spicy { get; set; }
        public bool Tobacco { get; set; }
        public bool Gourmand { get; set; }
        public int CurrentSize { get; set; } = 0;
        public int StockQuantity { get; set; } = 0;
        public string PriceInfo { get; set; } = "";
        public List<ProductSize>? ProductSizes { get; set; }
        public FragranceNote? FragranceNoteObj { get; set; }
        public string Status { get; set; } = "Available";

        public IList<SelectListItem> StatusList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Available", Text = "Available" },
            new SelectListItem { Value = "Out of Stock", Text = "Out of Stock" },
            new SelectListItem { Value = "Discontinued", Text = "Discontinued" }
        };

        public string Ingredient_Name_1 { get; set; } = "";
        public string Ingredient_Img_1 { get; set; } = "";
        public string Ingredient_Name_2 { get; set; } = "";
        public string Ingredient_Img_2 { get; set; } = "";
        public string Ingredient_Name_3 { get; set; } = "";
        public string Ingredient_Img_3 { get; set; } = "";
        public string Ingredient_Name_4 { get; set; } = "";
        public string Ingredient_Img_4 { get; set; } = "";

        public string ImageUrl { get; set; } = ""; // URL hình ảnh sản phẩm
        public string? DescriptionImages { get; set; }
        public List<string>? ListDescriptionImages
        {
            get
            {
                if (!string.IsNullOrEmpty(DescriptionImages))
                {
                    return JsonConvert.DeserializeObject<List<string>>(DescriptionImages);
                }
                return default;
            }
            set
            {
            }
        }

        public List<IFormFile> DescriptionImageFiles { get; set; } = new();
        public List<int> SaveImageIndexChange { get; set; } = new();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
    public class PerfumedNote
    {
        public List<string>? Top { get; set; }
        public List<string>? Middle { get; set; }
        public List<string>? Base { get; set; }
    }
}
