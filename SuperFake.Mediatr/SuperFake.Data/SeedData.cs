using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace SuperFake.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SuperFakeDbContext(serviceProvider.GetRequiredService<DbContextOptions<SuperFakeDbContext>>()))
            {
                // Look for any movies.
                if (context.Products.Any())
                {
                    return;   // DB has been seeded
                }

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Duff Beer (6 Pack of 12 cans)",
                        Category = "Beer",
                        Description = "6xer of decliciousness",
                        Price = 6M
                    },

                    new Product
                    {
                        Name = "Fudd Bear (32 oz can)",
                        Category = "Beer",
                        Description = "For when they are all out of Duff",
                        Price = 1.25M
                    },

                    new Product
                    {
                       Name = "Crusty Burger",
                       Category = "Food",
                       Description = "Hey, it's meat right?",
                       Price = 2.50M
                    }
                );

                context.Customers.AddRange(
                    new Customer
                    {
                        FirstName = "Homer",
                        LastName = "Simpson"
                    },

                    new Customer
                    {
                        FirstName = "Ned",
                        LastName = "Flanders"
                    },

                    new Customer
                    {
                        FirstName = "Montgomery",
                        LastName = "Burns"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
