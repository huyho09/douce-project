using Microsoft.AspNetCore.Mvc;
using ScentifyWebApp.Models;
using ScentifyWebApp.Services;

namespace ScentifyWebApp.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly CartService _cartService;

        public CartViewComponent(CartService cartService)
        {
            _cartService = cartService;
        }

        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    var cart = _cartService.GetCart();
        //    cart.Add(new CartItem
        //    {
        //        Product = new Product
        //        {
        //            Id = 1,
        //            Name = "Ouranon Eau de Parfum",
        //            Image = "./images/gloam.png",
        //            Description = "Frankincense, Hay, Myrrh",
        //            Size = "50 mL",
        //            Price = new PriceInfo
        //            {
        //                Amount = 165.00m,
        //                Currency = "EUR",
        //                UnitPrice = "€330.00 per 100 ml"
        //            }
        //        },
        //        Quantity = 1
        //    });

        //    cart.Add(new CartItem
        //    {
        //        Product = new Product
        //        {
        //            Id = 2,
        //            Name = "Aurora Mist",
        //            Image = "./images/per_1.png",
        //            Description = "Lavender, Citrus, Musk",
        //            Size = "100 mL",
        //            Price = new PriceInfo
        //            {
        //                Amount = 250.00m,
        //                Currency = "EUR",
        //                UnitPrice = "€250.00 per 100 ml"
        //            }
        //        },
        //        Quantity = 1
        //    });

        //    cart.Add(new CartItem
        //    {
        //        Product = new Product
        //        {
        //            Id = 3,
        //            Name = "Twilight Essence",
        //            Image = "./images/per_2.png",
        //            Description = "Rose, Sandalwood, Amber",
        //            Size = "75 mL",
        //            Price = new PriceInfo
        //            {
        //                Amount = 190.00m,
        //                Currency = "EUR",
        //                UnitPrice = "€253.33 per 100 ml"
        //            }
        //        },
        //        Quantity = 1
        //    });
        //    return View(cart);
        //}
    }
}
