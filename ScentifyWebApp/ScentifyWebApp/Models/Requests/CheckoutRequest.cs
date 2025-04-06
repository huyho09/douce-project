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
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Only Gmail addresses are allowed (e.g., user@gmail.com).")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^(?:\+84|0)(3[2-9]|5[6|8|9]|7[0|6-9]|8[1-9]|9[0-9])[0-9]{7}$",
            ErrorMessage = "Phone number must be a valid Vietnamese number (e.g., +84912345678 or 0912345678).")]
        public string PhoneNumber { get; set; }

        //[Required(ErrorMessage = "Country is required")]
        //public string Country { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters")]
        public string Address { get; set; }

        public List<CartItem> CartItems { get; set; }

        public CheckoutRequest() { }
        public CheckoutRequest(List<CartItem> cartItems)
        {
            CartItems = cartItems;
        }
    }
}
