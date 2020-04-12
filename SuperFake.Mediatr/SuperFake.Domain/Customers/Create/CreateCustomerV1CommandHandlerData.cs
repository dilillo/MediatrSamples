using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading.Tasks;

namespace SuperFake.Domains
{
    public interface ICreateCustomerV1CommandHandlerData
    {
        void AddCustomer(Customer customer);

        Task<bool> CustomerNameExists(string customerFirstName, string customerLastName);

        Task SaveChanges();
    }

    public class CreateCustomerV1CommandHandlerData : ICreateCustomerV1CommandHandlerData
    {
        private readonly SuperFakeDbContext _dbContext;

        public CreateCustomerV1CommandHandlerData(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCustomer(Customer customer) => _dbContext.Customers.Add(customer);

        public Task<bool> CustomerNameExists(string customerFirstName, string customerLastName)
            => _dbContext.Customers.AnyAsync(i => i.FirstName == customerFirstName && i.LastName == customerLastName);

        public Task SaveChanges() => _dbContext.SaveChangesAsync();
    }
}
