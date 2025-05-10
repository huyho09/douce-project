using ScentifyWebApp.Models;
using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Infrastructure.Services.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(List<CartItem> cartItems, CustomerInfo? customerInfo);
    }
}
