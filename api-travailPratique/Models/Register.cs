using System.ComponentModel.DataAnnotations;

namespace api_travailPratique.Models
{
    public enum Role
    {
        Client,
        Vendeur
 
    }
    public class Register
    {
        [Required(ErrorMessage = "Ce champ est requis !")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Ce champ est requis !")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Ce champ est requis !")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Ce champ est requis !")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Ce champ est requis !")]
        public Role? Role { get; set; }
    }
}
