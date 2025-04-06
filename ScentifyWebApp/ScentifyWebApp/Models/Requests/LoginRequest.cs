using System.ComponentModel.DataAnnotations;

namespace ScentifyWebApp.Models.Requests
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string ReturnValue { get; set; }

        public bool IsRememberMe { get; set; } = false;
    }
}
