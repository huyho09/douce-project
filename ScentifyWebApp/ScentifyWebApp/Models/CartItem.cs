using ScentifyWebApp.Models.Dtos;

namespace ScentifyWebApp.Models
{
    public class CartItem
    {
        public DtoPerfume Product { get; set; }
        public int Quantity { get; set; }
    }
}