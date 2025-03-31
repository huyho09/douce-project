using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Models.Dtos
{
    public class DtoPerfume
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Manufacturer { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Composition { get; set; }
        public List<Ingredient>? DtoIngredients { get; set; }
        public int StockQuantity { get; set; }
        public string TopPerfumed { get; set; }
        public string MiddlePerfumed { get; set; }
        public string BasePerfumed { get; set; }
        public List<PriceInfo>? DtoPriceInfo { get; set; }
        public string Status { get; set; }
        public string ImageUrl { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
        public List<DtoPerfume> SimilarPerfumes { get; set; }
		public dynamic FragranceNotes { get; set; }
	}
}
