using System.ComponentModel.DataAnnotations;

namespace Doctrina.WebUI.Models
{
    public class LoginModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
