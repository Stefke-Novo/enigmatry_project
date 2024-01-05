using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Tenant> Tenants { get; set; }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        public AppDbContext() : base() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    }
}
