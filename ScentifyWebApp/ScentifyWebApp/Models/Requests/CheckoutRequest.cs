using System.ComponentModel.DataAnnotations;

namespace ScentifyWebApp.Models.Requests
{
	public class CheckoutRequest
	{
		[Required(ErrorMessage = "Title is required")]
		public string Title { get; set; }

		[Required(ErrorMessage = "First name is required")]
		[StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last name is required")]
		[StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Email address is required")]
		public string EmaillAddress { get; set; }

		[Required(ErrorMessage = "Phone number is required")]
		public string PhoneNumber { get; set; }

		//[Required(ErrorMessage = "Country is required")]
		//public string Country { get; set; }

		[Required(ErrorMessage = "Address is required")]
		[StringLength(255, ErrorMessage = "Address cannot exceed 255 characters")]
		public string Address { get; set; }
	}
}
