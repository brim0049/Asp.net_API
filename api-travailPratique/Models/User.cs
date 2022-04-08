using System.ComponentModel.DataAnnotations;

namespace api_travailPratique.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Profil { get; set; }
        public decimal? Solde { get; set; }
    }
}
