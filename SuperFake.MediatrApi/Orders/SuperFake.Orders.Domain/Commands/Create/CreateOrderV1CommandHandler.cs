using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Orders.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Orders.Domain
{
    public class CreateOrderV1CommandHandler : IRequestHandler<CreateOrderV1Command>
    {
        private readonly SuperFakeOrdersDbContext _dbContext;
        private readonly IMediator _mediator;

        public CreateOrderV1CommandHandler(SuperFakeOrdersDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateOrderV1Command request, CancellationToken cancellationToken)
        {
            await VerifyCustomerExists(request.Order.CustomerID, cancellationToken);

            await CreateOrder(request.Order, cancellationToken);

            await PublishOrderCreatedNotification(request.Order, cancellationToken);

            return Unit.Value;
        }

        private async Task CreateOrder(Order order, CancellationToken cancellationToken)
        {
            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task PublishOrderCreatedNotification(Order order, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new OrderCreatedV1Notification
            {
                CustomerID = order.CustomerID,
                ID = order.ID,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus

            }, cancellationToken);
        }

        private async Task VerifyCustomerExists(int customerID, CancellationToken cancellationToken)
        {
            var customerExists = await _dbContext.Customers.AnyAsync(e => e.ID == customerID, cancellationToken);

            if (!customerExists)
                throw new CreateOrderCustomerDoesNotExistException();
        }
    }
}
