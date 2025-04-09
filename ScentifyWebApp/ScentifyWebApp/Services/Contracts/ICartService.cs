using ScentifyWebApp.Models;
using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Services.Contracts
{
    public interface ICartService
    {
        public List<CartItem> GetCart();

        public Task<Perfume?> AddToCart(string productId, int quantity, int volume);

        public Task<Perfume?> UpdateQuatity(string productId, int quantity);

        public Task<Perfume?> RemoveFromCart(string productId);

        public void ClearCart();
    }
}
