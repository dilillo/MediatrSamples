using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Orders.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Orders.Domain
{
    public class CustomerCreatedV1NotificationHandler : INotificationHandler<CustomerCreatedV1Notification>
    {
        private readonly SuperFakeOrdersDbContext _dbContext;

        public CustomerCreatedV1NotificationHandler(SuperFakeOrdersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CustomerCreatedV1Notification notification, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers.SingleOrDefaultAsync(i => i.ID == notification.ID, cancellationToken);

            if (customer != null)
                return;

            _dbContext.Customers.Add(new Customer { ID = notification.ID });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
