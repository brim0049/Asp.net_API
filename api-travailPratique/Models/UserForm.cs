using System.ComponentModel.DataAnnotations;

namespace api_travailPratique.Models
{
    public class UserForm
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Ce champ est requis !")]
        public string? Password { get; set; }
        public decimal? Solde { get; set; }

    }
}
