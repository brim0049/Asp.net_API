namespace api_travailPratique.Models
{
    public class Facture
    {
        public int Id { get; set; }
        // relation avec vendeur (many to one)
        public int VendeurId { get; set; }
        public Vendeur? Vendeur { get; set; }

        // relation avec client (many to one)
        public int ClientId { get; set; }
        public Client? Client { get; set; }

    }
}
