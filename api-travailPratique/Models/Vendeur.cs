namespace api_travailPratique.Models
{
    public class Vendeur:User
    {
        public Vendeur()
        {
            Produits = new List<Produit>();
            Factures = new List<Facture>();
        }
        // relation avec Produit (many to one)
        public ICollection<Produit> Produits { get; set; }
        // relation avec facture (many to one)
        public ICollection<Facture> Factures { get; set; }
    }
}
