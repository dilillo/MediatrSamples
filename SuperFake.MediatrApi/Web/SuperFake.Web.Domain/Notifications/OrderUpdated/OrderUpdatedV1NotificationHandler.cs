using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using SuperFake.Shared.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class OrderUpdatedV1NotificationHandler : INotificationHandler<OrderUpdatedV1Notification>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public OrderUpdatedV1NotificationHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(OrderUpdatedV1Notification notification, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.SingleOrDefaultAsync(i => i.ID == notification.CustomerID, cancellationToken);

            if (order != null)
            {
                _dbContext.Orders.Remove(order);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            order = new Order
            {
                CustomerID = notification.CustomerID,
                ID = notification.ID,
                OrderDate = notification.OrderDate,
                OrderStatus = notification.OrderStatus,
                OrderItems = notification.OrderItems.Select(i => new OrderItem
                {
                    ID = i.ID,
                    OrderID = i.OrderID,
                    ProductID = i.ProductID,
                    Quantity = i.Quantity

                }).ToList()
            };

            await PriceOrder(order, cancellationToken);

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task PriceOrder(Order order, CancellationToken cancellationToken)
        {
            var orderProductIds = order.OrderItems.Select(i => i.ProductID).ToList();

            var orderProducts = await _dbContext.Products.Where(i => orderProductIds.Contains(i.ID)).ToListAsync(cancellationToken);

            foreach (var orderItem in order.OrderItems)
            {
                var orderItemProduct = orderProducts.FirstOrDefault(i => i.ID == orderItem.ProductID);

                if (orderItemProduct == null)
                    continue;

                orderItem.TotalPrice = orderItemProduct.Price * orderItem.Quantity;
            }

            order.TotalPrice = order.OrderItems.Sum(i => i.TotalPrice);
        }
    }
}
