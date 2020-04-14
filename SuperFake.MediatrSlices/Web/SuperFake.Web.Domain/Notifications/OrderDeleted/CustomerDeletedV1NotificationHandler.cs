using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class OrderDeletedV1NotificationHandler : INotificationHandler<OrderDeletedV1Notification>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public OrderDeletedV1NotificationHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(OrderDeletedV1Notification notification, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.SingleOrDefaultAsync(i => i.ID == notification.ID, cancellationToken);

            if (order == null)
                return;

            _dbContext.Orders.Remove(order);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
