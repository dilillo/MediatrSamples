using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Orders.Data;
using SuperFake.Shared.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Orders.Domain
{
    public class DeleteOrderV1CommandHandler : IRequestHandler<DeleteOrderV1Command>
    {
        private readonly SuperFakeOrdersDbContext _dbContext;
        private readonly IMediator _mediator;

        public DeleteOrderV1CommandHandler(SuperFakeOrdersDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteOrderV1Command request, CancellationToken cancellationToken)
        {
            await VerifyOrderExists(request.OrderID);

            await VerifyOrderHasNotShipped(request.OrderID);

            await DeleteOrder(request.OrderID, cancellationToken);

            await PublishOrderDeletedNotification(request.OrderID, cancellationToken);

            return Unit.Value;
        }

        private async Task DeleteOrder(int orderID, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FindAsync(orderID);

            _dbContext.Orders.Remove(order);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task PublishOrderDeletedNotification(int orderID, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new OrderDeletedV1Notification { ID = orderID }, cancellationToken);
        }

        private async Task VerifyOrderExists(int orderID)
        {
            var orderExists = await _dbContext.Orders.AnyAsync(e => e.ID == orderID);

            if (!orderExists)
                throw new DeleteOrderDoesNotExistException();
        }

        private async Task VerifyOrderHasNotShipped(int orderID)
        {
            var orderIsShipped = await _dbContext.Orders.AnyAsync(i => i.ID == orderID && i.OrderStatus == OrderStatuses.Shipped);

            if (orderIsShipped)
                throw new DeleteOrderIsShippedAndCannotBeChangedException();
        }
    }
}
