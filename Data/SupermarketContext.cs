using System.Data.Entity;
using POS_SYSTEM.Models;

namespace POS_SYSTEM.Data
{
    public class SupermarketContext : DbContext
    {
        public SupermarketContext() : base("name=SupermarketContext") { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
    }
}