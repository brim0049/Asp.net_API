using Microsoft.EntityFrameworkCore;
namespace api_travailPratique.Models
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Client>? Clients { get; set; }
        public DbSet<Vendeur>? Vendeurs { get; set; }
        public DbSet<Produit>? Produits { get; set; }
        public DbSet<Facture>? Factures { get; set; }

      
       
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            // MySQL Community Server
            string ConnectionString = "server=localhost;port=3306;database=ApiDb;user=api_user;password=api_user";
            dbContextOptionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
        }

    }

}
