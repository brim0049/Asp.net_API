using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace api_travailPratique.Models
{
    public enum Role
    {
         Client =2,
         Vendeur =3
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
        [EnumDataType(typeof(Role))]
        [Required(ErrorMessage = "Ce champ est requis !")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role? Role { get; set; }
    }
}
