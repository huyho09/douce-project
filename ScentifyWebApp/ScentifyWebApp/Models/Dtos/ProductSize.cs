namespace ScentifyWebApp.Models.Dtos
{
    public class ProductSize
    {
        public string? ImageUrl { get; set; }
        public int VolumeMl { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; } = "VND";
    }
}
