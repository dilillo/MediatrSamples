using Microsoft.EntityFrameworkCore;

namespace SuperFake.Web.Data
{
    public class SuperFakeWebDbContext : DbContext
    {
        public SuperFakeWebDbContext(DbContextOptions<SuperFakeWebDbContext> options): base(options)
        { 
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Customer>()
                .Property(p => p.FullName)
                .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");
            
            modelBuilder
                .Entity<Product>()
                .HasIndex(i => i.Name);

            modelBuilder
                .Entity<Product>()
                .HasIndex(i => i.Category);

            modelBuilder
                .Entity<Product>()
                .Property(i => i.Price)
                .HasColumnType("decimal(18, 6)");

            modelBuilder
                .Entity<Order>()
                .Property(i => i.TotalPrice)
                .HasColumnType("decimal(18, 6)");

            modelBuilder
                .Entity<OrderItem>()
                .Property(i => i.TotalPrice)
                .HasColumnType("decimal(18, 6)");
        }
    }
}
