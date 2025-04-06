using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ScentifyWebApp.DAL.DB;
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

        public CartService(IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext context,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        public List<CartItem> GetCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString(CartSessionKey);
            if (cartJson == null)
            {
                return new List<CartItem>();
            }
            return JsonConvert.DeserializeObject<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }

        public async Task<Perfume?> AddToCart(string productId, int quantity)
        {
            try
            {
                var product = await RetrivePerfume(productId);
                if (product != null)
                {
                    var cart = GetCart();
                    var cartItem = cart?.FirstOrDefault(p => Guid.Parse(p.Product.Id) == product.Id);
                    if (cartItem == null)
                    {
                        var dtoProduct = _mapper.Map<DtoPerfume>(product);
                        cart.Add(new CartItem { Product = dtoProduct, Quantity = quantity });
                    }
                    else
                    {
                        cartItem.Quantity += quantity;
                    }
                    SaveCart(cart);
                    return product;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public async Task<Perfume?> UpdateQuatity(string productId, int quantity)
        {
            try
            {
                var product = await RetrivePerfume(productId);
                if (product != null)
                {
                    var cart = GetCart();
                    var cartItem = cart?.FirstOrDefault(p => Guid.Parse(p.Product.Id) == product.Id);
                    if (cartItem != null)
                    {
                        cartItem.Quantity = quantity;

                        SaveCart(cart);
                        return product;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public async Task<Perfume?> RemoveFromCart(string productId)
        {
            try
            {
                var cart = GetCart();

                var product = await RetrivePerfume(productId);
                if (product != null)
                {
                    var cartItem = cart.FirstOrDefault(p => Guid.Parse(p.Product.Id) == product.Id);
                    if (cartItem != null)
                    {
                        cart.Remove(cartItem);
                        SaveCart(cart);
                        return product;
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public void ClearCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.Remove(CartSessionKey);
        }

        private void SaveCart(List<CartItem> cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = JsonConvert.SerializeObject(cart);
            session.SetString(CartSessionKey, cartJson);
        }

        private async Task<Perfume?> RetrivePerfume(string productId)
        {
            if (!Guid.TryParse(productId, out Guid guidId))
            {
                return null;
            }
            var products = await _context.Perfume.ToListAsync();
            return products.FirstOrDefault(p => p.Id == guidId);
        }
    }
}
