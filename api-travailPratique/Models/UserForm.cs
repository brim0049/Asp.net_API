using System.ComponentModel.DataAnnotations;

namespace api_travailPratique.Models
{
    public class UserForm
    {
        [Required(ErrorMessage = "Ce champ est requis !")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Ce champ est requis !")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Ce champ est requis !")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Ce champ est requis !")]
        public string? Password { get; set; }
    }
}
