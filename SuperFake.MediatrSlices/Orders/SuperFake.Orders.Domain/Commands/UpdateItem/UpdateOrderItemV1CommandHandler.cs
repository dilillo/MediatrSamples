using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Orders.Data;
using SuperFake.Shared.Data;
using SuperFake.Shared.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Orders.Domain
{
    public class UpdateOrderItemV1CommandHandler : IRequestHandler<UpdateOrderItemV1Command>
    {
        private readonly SuperFakeOrdersDbContext _dbContext;
        private readonly IMediator _mediator;

        public UpdateOrderItemV1CommandHandler(SuperFakeOrdersDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateOrderItemV1Command request, CancellationToken cancellationToken)
        {
            await VerifyOrderItemExists(request.OrderItem.ID, cancellationToken);

            await VerifyOrderExists(request.OrderItem.OrderID, cancellationToken);

            await VerifyOrderHasNotShipped(request.OrderItem.OrderID, cancellationToken);

            await VerifyProductExists(request.OrderItem.ProductID, cancellationToken);

            await UpdateOrderItem(request, cancellationToken);

            await PublishOrderUpdatedNotification(request.OrderItem.OrderID, cancellationToken);

            return Unit.Value;
        }

        private async Task UpdateOrderItem(UpdateOrderItemV1Command request, CancellationToken cancellationToken)
        {
            _dbContext.Update(request.OrderItem);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task PublishOrderUpdatedNotification(int orderID, CancellationToken cancellationToken)
        {
            var orderWithItems = await _dbContext.Orders
                .Include(i => i.OrderItems)
                .SingleAsync(i => i.ID == orderID, cancellationToken);

            await _mediator.Publish(new OrderUpdatedV1Notification
            {
                CustomerID = orderWithItems.CustomerID,
                ID = orderWithItems.ID,
                OrderDate = orderWithItems.OrderDate,
                OrderStatus = orderWithItems.OrderStatus,
                OrderItems = orderWithItems.OrderItems.Select(i => new OrderUpdatedV1Notification.OrderUpdatedOrderItem
                {
                    ID = i.ID,
                    OrderID = i.OrderID,
                    ProductID = i.ProductID,
                    Quantity = i.Quantity

                }).ToList()

            }, cancellationToken);
        }

        private async Task VerifyOrderExists(int orderID, CancellationToken cancellationToken)
        {
            var orderExists = await _dbContext.Orders.AnyAsync(e => e.ID == orderID, cancellationToken);

            if (!orderExists)
                throw new UpdateOrderItemOrderDoesNotExistException();
        }

        private async Task VerifyProductExists(int productID, CancellationToken cancellationToken)
        {
            var productExists = await _dbContext.Products.AnyAsync(e => e.ID == productID, cancellationToken);

            if (!productExists)
                throw new UpdateOrderItemProductDoesNotExistException();
        }

        private async Task VerifyOrderItemExists(int orderItemID, CancellationToken cancellationToken)
        {
            var orderItemExists = await _dbContext.OrderItems.AnyAsync(e => e.ID == orderItemID, cancellationToken);

            if (!orderItemExists)
                throw new UpdateOrderItemDoesNotExistException();
        }

        private async Task VerifyOrderHasNotShipped(int orderID, CancellationToken cancellationToken)
        {
            var orderIsShipped = await _dbContext.Orders.AnyAsync(i => i.ID == orderID && i.OrderStatus == OrderStatuses.Shipped, cancellationToken);

            if (orderIsShipped)
                throw new UpdateOrderItemOrderIsShippedAndCannotBeChangedException();
        }
    }
}
