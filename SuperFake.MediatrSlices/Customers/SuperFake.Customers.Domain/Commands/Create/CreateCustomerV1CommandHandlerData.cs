using Microsoft.EntityFrameworkCore;
using SuperFake.Customers.Data;
using System.Threading.Tasks;

namespace SuperFake.Customers.Domain
{
    public interface ICreateCustomerV1CommandHandlerData
    {
        void AddCustomer(Customer customer);

        Task<bool> CustomerNameExists(string customerFirstName, string customerLastName);

        Task SaveChanges();
    }

    public class CreateCustomerV1CommandHandlerData : ICreateCustomerV1CommandHandlerData
    {
        private readonly SuperFakeCustomersDbContext _dbContext;

        public CreateCustomerV1CommandHandlerData(SuperFakeCustomersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCustomer(Customer customer) => _dbContext.Customers.Add(customer);

        public Task<bool> CustomerNameExists(string customerFirstName, string customerLastName)
            => _dbContext.Customers.AnyAsync(i => i.FirstName == customerFirstName && i.LastName == customerLastName);

        public Task SaveChanges() => _dbContext.SaveChangesAsync();
    }
}
