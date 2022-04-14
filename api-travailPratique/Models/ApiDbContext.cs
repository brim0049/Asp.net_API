using Microsoft.EntityFrameworkCore;
namespace api_travailPratique.Models
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Client>? Clients { get; set; }
        public DbSet<Vendeur>? Vendeurs { get; set; }
        public DbSet<Produit>? Produits { get; set; }
        public DbSet<Facture>? Factures { get; set; }

        /*
        public DbSet<Login>? Logins { get; set; }
        public DbSet<Register>? Registers { get; set; } */


        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            // MySQL Community Server
            string ConnectionString = "server=localhost;port=3306;database=apidb;user=api_user;password=api_user";
            dbContextOptionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
        }

    }

}
