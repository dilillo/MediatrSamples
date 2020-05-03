using Microsoft.EntityFrameworkCore;
using SuperFake.Customers.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Customers.Domain
{
    public interface ICreateCustomerV1CommandHandlerData
    {
        void AddCustomer(Customer customer);

        Task<bool> CustomerNameExists(string customerFirstName, string customerLastName, CancellationToken cancellationToken);

        Task SaveChanges(CancellationToken cancellationToken);
    }

    public class CreateCustomerV1CommandHandlerData : ICreateCustomerV1CommandHandlerData
    {
        private readonly SuperFakeCustomersDbContext _dbContext;

        public CreateCustomerV1CommandHandlerData(SuperFakeCustomersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCustomer(Customer customer) => _dbContext.Customers.Add(customer);

        public Task<bool> CustomerNameExists(string customerFirstName, string customerLastName, CancellationToken cancellationToken)
            => _dbContext.Customers.AnyAsync(i => i.FirstName == customerFirstName && i.LastName == customerLastName, cancellationToken);

        public Task SaveChanges(CancellationToken cancellationToken) => _dbContext.SaveChangesAsync(cancellationToken);
    }
}
