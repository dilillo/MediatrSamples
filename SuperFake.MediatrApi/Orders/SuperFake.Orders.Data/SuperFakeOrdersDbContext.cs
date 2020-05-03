using Microsoft.EntityFrameworkCore;

namespace SuperFake.Orders.Data
{
    public class SuperFakeOrdersDbContext : DbContext
    {
        public SuperFakeOrdersDbContext(DbContextOptions<SuperFakeOrdersDbContext> options): base(options)
        { 
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
