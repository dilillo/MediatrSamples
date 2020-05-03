using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Customers.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Customers.Domain
{
    public class OrderCreatedV1NotificationHandler : INotificationHandler<OrderCreatedV1Notification>
    {
        private readonly SuperFakeCustomersDbContext _dbContext;

        public OrderCreatedV1NotificationHandler(SuperFakeCustomersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(OrderCreatedV1Notification notification, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers.SingleOrDefaultAsync(i => i.ID == notification.CustomerID && i.HasOrders == false, cancellationToken);

            if (customer == null)
                return;

            customer.HasOrders = true;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
