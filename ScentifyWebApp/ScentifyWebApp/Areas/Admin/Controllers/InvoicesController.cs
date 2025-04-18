using AutoMapper;
using Azure.Core.GeoJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ScentifyWebApp.Authorization.BaseController;
using ScentifyWebApp.DAL.DB;
using ScentifyWebApp.Libs;
using ScentifyWebApp.Models.Dtos;
using ScentifyWebApp.Models.Entities;
using ScentifyWebApp.Models.ViewModels;

namespace ScentifyWebApp.Areas.Admin.Controllers
{
    public class InvoicesController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public InvoicesController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var invoices = await _context.Invoice.AsNoTracking()
                .OrderByDescending(m => m.PaymentDate).Select(p => new DtoInvoice
                {
                    Id = p.Id.ToString(),
                    DtoCustomerInfo = !string.IsNullOrWhiteSpace(p.CustomerInfo)
                            ? JsonConvert.DeserializeObject<CustomerInfo>(p.CustomerInfo)
                            : new CustomerInfo(),
                    Status = p.Status,
                    PaymentDate = p.PaymentDate.ToString("HH:mm dd-MM-yyyy"),
                    DtoInvoiceItems = !string.IsNullOrWhiteSpace(p.InvoiceItems)
                            ? JsonConvert.DeserializeObject<List<InvoiceItem>>(p.InvoiceItems)
                            : new List<InvoiceItem>()
                })
            .ToListAsync();

            return View(invoices);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInvoiceStatus(List<string> invoiceIds, int status)
        {
            try
            {
                var lstId = invoiceIds.Select(m => Guid.Parse(m));
                var invoices = await _context.Invoice.Where(m => lstId.Contains(m.Id)).ToListAsync();
                if (invoices != null)
                {
                    invoices.ForEach(m => m.Status = status);
                    await _context.SaveChangesAsync();
                    return Json(new { status = 200, message = "Thành công!" });
                }
            }
            catch (Exception ex)
            {
            }
            return Json(new { status = 400, message = "Thất bại!" });
        }
    }
}
