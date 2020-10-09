using System.ComponentModel.DataAnnotations;

namespace Tournament.Core.Models
{
    public class LoginRequest
    {

        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}