using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Customers.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Customers.Domain
{
    public class DeleteCustomerV1CommandHandler : IRequestHandler<DeleteCustomerV1Command>
    {
        private readonly SuperFakeCustomersDbContext _dbContext;
        private readonly IMediator _mediator;

        public DeleteCustomerV1CommandHandler(SuperFakeCustomersDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteCustomerV1Command request, CancellationToken cancellationToken)
        {
            await VerifyCustomerExists(request.CustomerID);

            await VerifyCustomerHasNoOrders(request.CustomerID);

            var customer = await _dbContext.Customers.FindAsync(request.CustomerID);

            _dbContext.Customers.Remove(customer);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await PublishCustomerDeletedNotification(request.CustomerID, cancellationToken);

            return Unit.Value;
        }

        private async Task PublishCustomerDeletedNotification(int customerID, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new CustomerDeletedV1Notification
            {
                ID = customerID

            }, cancellationToken);
        }

        private async Task VerifyCustomerHasNoOrders(int customerID)
        {
            var customerHasOrders = await _dbContext.Customers.AnyAsync(i => i.ID == customerID && i.HasOrders);

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
