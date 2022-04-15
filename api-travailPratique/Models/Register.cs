using System.ComponentModel.DataAnnotations;

namespace api_travailPratique.Models
{
    public class Register
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}
