using ScentifyWebApp.Models;
using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Services.Contracts
{
    public interface ICartService
    {
        public List<CartItem> GetCart();

        public Task<Perfume?> AddToCart(string productId, int quantity, int volume);

        public Task<Perfume?> UpdateQuantity(string productId, int quantity, int volume);

        public Task<Perfume?> RemoveFromCart(string productId, int volume);

        public void ClearCart();
    }
}
