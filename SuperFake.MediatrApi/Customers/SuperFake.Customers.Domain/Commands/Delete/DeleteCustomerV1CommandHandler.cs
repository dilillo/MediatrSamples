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
            await VerifyCustomerExists(request.CustomerID, cancellationToken);

            await VerifyCustomerHasNoOrders(request.CustomerID, cancellationToken);

            await DeleteCustomer(request.CustomerID, cancellationToken);

            await PublishCustomerDeletedNotification(request.CustomerID, cancellationToken);

            return Unit.Value;
        }

        private async Task DeleteCustomer(int customerID, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers.FindAsync(customerID, cancellationToken);

            _dbContext.Customers.Remove(customer);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task PublishCustomerDeletedNotification(int customerID, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new CustomerDeletedV1Notification
            {
                ID = customerID

            }, cancellationToken);
        }

        private async Task VerifyCustomerHasNoOrders(int customerID, CancellationToken cancellationToken)
        {
            var customerHasOrders = await _dbContext.Customers.AnyAsync(i => i.ID == customerID && i.HasOrders, cancellationToken);

            if (customerHasOrders)
                throw new DeleteCustomerWithOrdersCannotBeDeletedException();
        }

        private async Task VerifyCustomerExists(int customerID, CancellationToken cancellationToken)
        {
            var customerExists = await _dbContext.Customers.AnyAsync(e => e.ID == customerID, cancellationToken);

            if (!customerExists)
                throw new DeleteCustomerDoesNotExistException();
        }
    }
}
