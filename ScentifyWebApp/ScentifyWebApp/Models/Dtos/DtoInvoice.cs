using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Models.Dtos
{
    public class DtoInvoice
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Auto-generate GUID

        public CustomerInfo DtoCustomerInfo { get; set; } // JSON string storing customer info

        public List<InvoiceItem> DtoInvoiceItems { get; set; } // JSON list string storing invoice items

        public string PaymentDate { get; set; } // Consider changing to DateTime if needed

        public string PaymentMethod { get; set; }

        public int Status { get; set; }

        public decimal OtherCost { get; set; } = 0;

        public decimal Shipping { get; set; } = 0;

        public decimal Discount { get; set; } = 0;

        public string Noted { get; set; }
    }

}
