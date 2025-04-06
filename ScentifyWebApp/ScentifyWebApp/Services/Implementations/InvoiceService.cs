using ScentifyWebApp.DAL.DB;
using ScentifyWebApp.Models.Entities;
using ScentifyWebApp.Services.Contracts;

namespace ScentifyWebApp.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _context;

        public InvoiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InsertInvoiceAsync(Invoice invoice)
        {
            //await _context.Invoice.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }
    }
}
