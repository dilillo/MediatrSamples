using Microsoft.EntityFrameworkCore;

namespace SuperFake.Products.Data
{
    public class SuperFakeProductsDbContext : DbContext
    {
        public SuperFakeProductsDbContext(DbContextOptions<SuperFakeProductsDbContext> options): base(options)
        { 
        }

        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Product>()
                .Property(i => i.Price)
                .HasColumnType("decimal(18, 6)");
        }
    }
}
