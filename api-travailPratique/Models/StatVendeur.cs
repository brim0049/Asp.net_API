namespace api_travailPratique.Models
{
    public class StatVendeur
    {
        public int Id { get; set; }
        public double TotalSomme { get; set; }
        public double Benefice { get; set; }
        public int NombreArticleVendus { get; set; }
        public int? VendeurId { get; set; }
    }
}
