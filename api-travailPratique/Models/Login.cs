using System.ComponentModel.DataAnnotations;

namespace api_travailPratique.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Ce champ est requis !")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Ce champ est requis !")]
        public string? Password { get; set; }
    }
}
