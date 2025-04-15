using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using ScentifyWebApp.DAL.DB;
using ScentifyWebApp.Libs;
using ScentifyWebApp.Models;
using ScentifyWebApp.Models.Dtos;
using ScentifyWebApp.Models.Entities;
using ScentifyWebApp.Services.Contracts;

namespace ScentifyWebApp.Services.Implementations
{
    public class CartService : ICartService
    {
        private const string CartSessionKey = "Cart";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        public List<CartItem> GetCart()
        {
            var cartJson = _httpContextAccessor.HttpContext?.Session.GetString(CartSessionKey);
            return string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }

        public async Task<Perfume?> AddToCart(string productId, int quantity, int volume)
        {
            var product = await RetrievePerfumeAsync(productId);
            if (product == null) return null;

            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(p => Guid.Parse(p.Product.Id) == product.Id && p.VolumeMl == volume);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var dto = _mapper.Map<DtoPerfume>(product);
                dto.CurrentSize = volume;

                dto.ProductSizes = JsonHelpers.ParseJson<ProductSize>(dto.PriceInfo) ?? new List<ProductSize>();
                cart.Add(new CartItem { Product = dto, Quantity = quantity, VolumeMl = volume });
            }

            SaveCart(cart);
            return product;
        }

        public async Task<Perfume?> UpdateQuantity(string productId, int quantity, int volume)
        {
            var product = await RetrievePerfumeAsync(productId);
            if (product == null) return null;

            var cart = GetCart();
            var cartItem = cart.FirstOrDefault(p => Guid.Parse(p.Product.Id) == product.Id && p.VolumeMl == volume);

            if (cartItem == null) return null;

            cartItem.Quantity = quantity;
            SaveCart(cart);
            return product;
        }

        public async Task<Perfume?> RemoveFromCart(string productId, int volume)
        {
            var product = await RetrievePerfumeAsync(productId);
            if (product == null) return null;

            var cart = GetCart();
            var cartItem = cart.FirstOrDefault(p => Guid.Parse(p.Product.Id) == product.Id && p.VolumeMl == volume);

            if (cartItem == null) return null;

            cart.Remove(cartItem);
            SaveCart(cart);
            return product;
        }

        public void ClearCart()
        {
            _httpContextAccessor.HttpContext?.Session.Remove(CartSessionKey);
        }

        private void SaveCart(List<CartItem> cart)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null) return;

            var cartJson = JsonConvert.SerializeObject(cart);
            session.SetString(CartSessionKey, cartJson);
        }

        private async Task<Perfume?> RetrievePerfumeAsync(string productId)
        {
            if(Guid.TryParse(productId, out var guidId))
            {
                var perfumes = await _context.Perfume
                                .AsNoTracking()
                                .Where(m => m.Id == guidId)
                                .Select(p => new Perfume
                                {
                                    Id = p.Id,
                                    Name = p.Name,
                                    ImageUrl = p.ImageUrl,
                                    PriceInfo = p.PriceInfo

                                }).ToListAsync();
                return perfumes.FirstOrDefault();
            }
            return null;
        }
    }

}
