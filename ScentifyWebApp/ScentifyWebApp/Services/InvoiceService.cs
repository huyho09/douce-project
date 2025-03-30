using ScentifyWebApp.DAL.DB;
using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Services
{
    public class InvoiceService
    {
        private readonly ApplicationDbContext _context;

        public InvoiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InsertInvoiceAsync(Invoice invoice)
        {
            await _context.Invoice.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }
    }
}
