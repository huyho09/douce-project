using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ScentifyWebApp.Models.Entities
{
    [Index(nameof(PaymentDate))]
    [Index(nameof(Status))]
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); // Auto-generate GUID

        [Required]
        public string CustomerInfo { get; set; } // JSON string storing customer info

        [Required]
        public string InvoiceItems { get; set; } // JSON list string storing invoice items

        public DateTime PaymentDate { get; set; }

        [StringLength(200)]
        public string PaymentMethod { get; set; }

        [Required]
        public int Status { get; set; }

        [Range(0, double.MaxValue)]
        public decimal OtherCost { get; set; } = 0;

        [Range(0, double.MaxValue)]
        public decimal Shipping { get; set; } = 0;

        [Range(0, double.MaxValue)]
        public decimal Discount { get; set; } = 0;

        public string Noted { get; set; }
    }
    public class CustomerInfo
    {
        public string FullName { get; set; }
        //public string Country { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
    public class InvoiceItem
    {
        public string PerfumeId { get; set; }
        public string PerfumeName { get; set; }
        public int Quantity { get; set; }
        public int VolumeMl { get; set; }
        public decimal Price { get; set; }

        public InvoiceItem()
        {
        }

        public InvoiceItem(CartItem cartItem)
        {
            PerfumeId = cartItem.Product.Id;
            PerfumeName = cartItem.Product.Name;
            Quantity = cartItem.Quantity;
            VolumeMl = cartItem.VolumeMl;
            Price = cartItem.Product.ProductSizes?.FirstOrDefault(m=>m.VolumeMl == VolumeMl)?.Price ?? 0;
        }
    }
}
