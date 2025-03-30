using ScentifyWebApp.Models.Dtos;
using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Models
{
    public class CartItem : PriceInfo
    {
        public DtoPerfume Product { get; set; }
        public int Quantity { get; set; }
    }
}