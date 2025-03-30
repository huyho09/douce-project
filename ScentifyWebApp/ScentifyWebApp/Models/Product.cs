using System.IO;

namespace ScentifyWebApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public PriceInfo Price { get; set; }
    }

    public class PriceInfo
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string UnitPrice { get; set; }
    }
}
