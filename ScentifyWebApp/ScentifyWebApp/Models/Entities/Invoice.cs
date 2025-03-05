using System.ComponentModel.DataAnnotations;

namespace ScentifyWebApp.Models.Entities
{
	public class Invoice
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid(); // Auto-generate GUID

		[Required]
		public string CustomerInfo { get; set; } // JSON string storing customer info

		[Required]
		public string InvoiceItems { get; set; } // JSON list string storing invoice items

		[StringLength(50)]
		public string PaymentDate { get; set; } // Consider changing to DateTime if needed

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
		public string Title { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Country { get; set; }
		public string Address { get; set; }
        public string Email { get; set; }
		public string Phone { get; set; }
	}
	public class InvoiceItem
	{
		public int Id { get; set; }
		public Guid PerfumeId { get; set; }
		public int Quantity { get; set; }
		public string Description { get; set; }
	}
}
