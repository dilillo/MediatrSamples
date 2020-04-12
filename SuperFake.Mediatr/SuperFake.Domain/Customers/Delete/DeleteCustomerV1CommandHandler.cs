using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Domains
{
    public class DeleteCustomerV1CommandHandler : IRequestHandler<DeleteCustomerV1Command>
    {
        private readonly SuperFakeDbContext _dbContext;

        public DeleteCustomerV1CommandHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteCustomerV1Command request, CancellationToken cancellationToken)
        {
            await VerifyCustomerExists(request.CustomerID);

            await VerifyCustomerHasNoOrders(request.CustomerID);

            var customer = await _dbContext.Customers.FindAsync(request.CustomerID);

            _dbContext.Customers.Remove(customer);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task VerifyCustomerHasNoOrders(int customerID)
        {
            var customerHasOrders = await _dbContext.Orders.AnyAsync(i => i.CustomerID == customerID);

            if (customerHasOrders)
                throw new DeleteCustomerWithOrdersCannotBeDeletedException();
        }

        private async Task VerifyCustomerExists(int customerID)
        {
            var customerExists = await _dbContext.Customers.AnyAsync(e => e.ID == customerID);

            if (!customerExists)
                throw new DeleteCustomerDoesNotExistException();
        }
    }
}
