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
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)  // Configura la relación de clave foránea
                .OnDelete(DeleteBehavior.Cascade);      
        }

    }
}
