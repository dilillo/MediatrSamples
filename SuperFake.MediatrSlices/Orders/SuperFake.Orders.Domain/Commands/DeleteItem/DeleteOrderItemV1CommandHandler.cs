﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Orders.Data;
using SuperFake.Shared.Data;
using SuperFake.Shared.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Orders.Domain
{
    public class DeleteOrderItemV1CommandHandler : IRequestHandler<DeleteOrderItemV1Command>
    {
        private readonly SuperFakeOrdersDbContext _dbContext;
        private readonly IMediator _mediator;

        public DeleteOrderItemV1CommandHandler(SuperFakeOrdersDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteOrderItemV1Command request, CancellationToken cancellationToken)
        {
            await VerifyOrderItemExists(request.OrderItemID, cancellationToken);

            var orderItem = await GetOrderItem(request, cancellationToken);

            await VerifyOrderExists(orderItem.OrderID, cancellationToken);

            await VerifyOrderHasNotShipped(orderItem.OrderID, cancellationToken);

            await DeleteOrderItem(orderItem, cancellationToken);

            await PublishOrderUpdatedNotification(orderItem.OrderID, cancellationToken);

            return Unit.Value;
        }

        private async Task DeleteOrderItem(OrderItem orderItem, CancellationToken cancellationToken)
        {
            _dbContext.OrderItems.Remove(orderItem);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task<OrderItem> GetOrderItem(DeleteOrderItemV1Command request, CancellationToken cancellationToken)
        {
            return await _dbContext.OrderItems.FindAsync(request.OrderItemID, cancellationToken);
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

        private async Task VerifyOrderItemExists(int orderItemID, CancellationToken cancellationToken)
        {
            var orderItemExists = await _dbContext.OrderItems.AnyAsync(e => e.ID == orderItemID, cancellationToken);

            if (!orderItemExists)
                throw new DeleteOrderItemDoesNotExistException();
        }

        private async Task VerifyOrderExists(int orderID, CancellationToken cancellationToken)
        {
            var orderExists = await _dbContext.Orders.AnyAsync(e => e.ID == orderID, cancellationToken);

            if (!orderExists)
                throw new DeleteOrderItemOrderDoesNotExistException();
        }

        private async Task VerifyOrderHasNotShipped(int orderID, CancellationToken cancellationToken)
        {
            var orderIsShipped = await _dbContext.Orders.AnyAsync(i => i.ID == orderID && i.OrderStatus == OrderStatuses.Shipped, cancellationToken);

            if (orderIsShipped)
                throw new DeleteOrderItemOrderIsShippedAndCannotBeChangedException();
        }
    }
}
