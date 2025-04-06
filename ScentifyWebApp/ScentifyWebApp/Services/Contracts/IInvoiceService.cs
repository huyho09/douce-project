using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Services.Contracts
{
    public interface IInvoiceService
    {
        public Task InsertInvoiceAsync(Invoice invoice);
    }
}
