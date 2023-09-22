using System.ComponentModel.DataAnnotations;

namespace BloggWebSite.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6,ErrorMessage = "Password shoul be atleast 6 characters")]
        public string Password { get; set; }
    }
}