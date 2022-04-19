namespace api_travailPratique.Models
{
    public class StatClient
    {
        public int Id { get; set; }
        public double TotalSommeDepensees { get; set; }
        public int NombreArticleAchetes { get; set; }
        public int? ClientId { get; set; }
    }
}
