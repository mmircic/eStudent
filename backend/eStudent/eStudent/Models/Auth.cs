using System.ComponentModel.DataAnnotations;

namespace eStudent.Models
{
    public class Auth
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
