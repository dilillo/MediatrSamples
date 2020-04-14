using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Customers.Domain
{
    public class OrderCreatedV1NotificationHandler : INotificationHandler<OrderCreatedV1Notification>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public OrderCreatedV1NotificationHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(OrderCreatedV1Notification notification, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.SingleOrDefaultAsync(i => i.ID == notification.CustomerID, cancellationToken);

            if (order != null)
                return;

            _dbContext.Orders.Add(new Order
            { 
                CustomerID = notification.CustomerID,
                ID = notification.ID,
                OrderDate = notification.OrderDate,
                OrderStatus = notification.OrderStatus
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
