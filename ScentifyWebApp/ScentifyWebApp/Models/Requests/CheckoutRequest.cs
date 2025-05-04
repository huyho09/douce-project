using System.ComponentModel.DataAnnotations;

namespace ScentifyWebApp.Models.Requests
{
    public class CheckoutRequest
    {
        //[Required(ErrorMessage = "Title is required")]
        //public string Title { get; set; }

        //[Required(ErrorMessage = "First name is required")]
        //[StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        //public string FirstName { get; set; }

        //[Required(ErrorMessage = "Last name is required")]
        //[StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        //public string LastName { get; set; }

        [Required(ErrorMessage = "Họ Tên đầy đủ không được để trống!")]
        public string FullName { get; set; }

        //[Required(ErrorMessage = "Email address is required")]
        //[RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Only Gmail addresses are allowed (e.g., user@gmail.com).")]
        //public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [RegularExpression(@"^(?:\+84|0)(3[2-9]|5[6|8|9]|7[0|6-9]|8[1-9]|9[0-9])[0-9]{7}$",
            ErrorMessage = "Số điện thoại không hợp lệ! (vd: 0912345678).")]
        public string PhoneNumber { get; set; }

        //[Required(ErrorMessage = "Country is required")]
        //public string Country { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống!")]
        //[StringLength(255, ErrorMessage = "Address cannot exceed 255 characters")]
        public string Address { get; set; }

        public List<CartItem> CartItems { get; set; }

        public bool IsSuccessed { get; set; } = false;

        public CheckoutRequest() { }
        public CheckoutRequest(List<CartItem> cartItems)
        {
            CartItems = cartItems;
        }
    }
}
