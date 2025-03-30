using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Models
{
    public class CartItem
    {
        public Perfume Product { get; set; }
        public int Quantity { get; set; }
    }
}