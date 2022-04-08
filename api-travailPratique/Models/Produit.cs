namespace api_travailPratique.Models
{
    public class Produit
    {
        public Produit()
        {
            Clients = new List<Client>();
        }
        public int Id { get; set; }
        public int Quantite { get; set; }
        public decimal Price { get; set; }
        // relation avec vendeur (many to one)
        public int? VendeurId { get; set; }
        public Vendeur? Vendeur { get; set; }

        //relation avec client (many to many)
        public ICollection<Client> Clients { get; set; }
    }
}
