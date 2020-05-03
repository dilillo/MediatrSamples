using Microsoft.EntityFrameworkCore;

namespace SuperFake.Customers.Data
{
    public class SuperFakeCustomersDbContext : DbContext
    {
        public SuperFakeCustomersDbContext()
        {
        }

        public SuperFakeCustomersDbContext(DbContextOptions<SuperFakeCustomersDbContext> options): base(options)
        { 
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
