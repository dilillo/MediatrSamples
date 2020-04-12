using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperFake.Business
{
    public class CustomerBusiness
    {
        private readonly SuperFakeDbContext _dbContext;

        public CustomerBusiness(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Customer>> GetAllCustomers()
        {
            return _dbContext.Customers
                .Include(i => i.Orders)
                .ToListAsync();
        }

        public Task<bool> CustomerExists(int customerID)
        {
            return _dbContext.Customers
                .AnyAsync(e => e.ID == customerID);
        }

        public Task<Customer> GetCustomerDetails(int customerID)
        {
            return _dbContext.Customers
                .Include(i => i.Orders)
                    .ThenInclude(i => i.OrderItems)
                        .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(i => i.ID == customerID);
        }

        public async Task CreateCustomer(Customer customer)
        {
            await VerifyCustomerNameIsUnique(customer);

            _dbContext.Customers.Add(customer);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCustomer(Customer customer)
        {
            await VerifyCustomerExists(customer.ID);

            await VerifyCustomerNameIsUnique(customer);

            _dbContext.Update(customer);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCustomer(int customerID)
        {
            await VerifyCustomerExists(customerID);

            await VerifyCustomerHasNoOrders(customerID);

            var customer = await _dbContext.Customers.FindAsync(customerID);

            _dbContext.Customers.Remove(customer);

            await _dbContext.SaveChangesAsync();
        }

        private async Task VerifyCustomerHasNoOrders(int customerID)
        {
            var customerHasOrders = await _dbContext.Orders.AnyAsync(i => i.CustomerID == customerID);

            if (customerHasOrders)
                throw new CustomerWithOrdersCannotBeDeletedException();
        }

        private async Task VerifyCustomerNameIsUnique(Customer customer)
        {
            var nameExists = await _dbContext.Customers.AnyAsync(i => i.ID != customer.ID && i.FirstName == customer.FirstName && i.LastName == customer.LastName);

            if (nameExists)
                throw new CustomerNameMustBeUniqueException();
        }

        private async Task VerifyCustomerExists(int customerID)
        {
            var customerExists = await _dbContext.Customers.AnyAsync(e => e.ID == customerID);

            if (!customerExists)
                throw new CustomerDoesNotExistException();
        }
    }
}
