using API.FurnitureStore.Models;
using Microsoft.EntityFrameworkCore;

namespace API.FurnitureStore.Data
{
    public class FugnitureStoreDbContext : DbContext
    {
        public FugnitureStoreDbContext(DbContextOptions options) : base(options) { }


        DbSet<Client> Clients { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
        }
    }
}
