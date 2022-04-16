using System.ComponentModel.DataAnnotations;

namespace api_travailPratique.Models
{
    public class ProduitForm
    {
        [Required(ErrorMessage = "Ce champ est requis !")]
        public string? NomProduit { get; set; }
        [Required(ErrorMessage = "Ce champ est requis !")]
        public int Quantite { get; set; }
        [Required(ErrorMessage = "Ce champ est requis !")]
        public decimal Price { get; set; }
    }
}
