using System.ComponentModel.DataAnnotations;

namespace BloggWebSite.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Passwords has to be minimum of 6 characters")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}