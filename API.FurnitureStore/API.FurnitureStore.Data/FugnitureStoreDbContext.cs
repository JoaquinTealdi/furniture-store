using API.FurnitureStore.Models;
using Microsoft.EntityFrameworkCore;

namespace API.FurnitureStore.Data
{
    public class FugnitureStoreDbContext : DbContext
    {
        public FugnitureStoreDbContext(DbContextOptions options) : base(options) { }


        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
        }
    }
}
