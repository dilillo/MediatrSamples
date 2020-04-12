using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Domains
{
    public class CreateOrderV1CommandHandler : IRequestHandler<CreateOrderV1Command>
    {
        private readonly SuperFakeDbContext _dbContext;

        public CreateOrderV1CommandHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateOrderV1Command request, CancellationToken cancellationToken)
        {
            await VerifyCustomerExists(request.Order.CustomerID);

            _dbContext.Orders.Add(request.Order);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task VerifyCustomerExists(int customerID)
        {
            var customerExists = await _dbContext.Customers.AnyAsync(e => e.ID == customerID);

            if (!customerExists)
                throw new CreateOrderCustomerDoesNotExistException();
        }
    }
}
