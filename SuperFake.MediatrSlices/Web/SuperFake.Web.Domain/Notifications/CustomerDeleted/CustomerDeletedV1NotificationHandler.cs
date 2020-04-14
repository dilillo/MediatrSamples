using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class CustomerDeletedV1NotificationHandler : INotificationHandler<CustomerDeletedV1Notification>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public CustomerDeletedV1NotificationHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CustomerDeletedV1Notification notification, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers.SingleOrDefaultAsync(i => i.ID == notification.ID, cancellationToken);

            if (customer == null)
                return;

            _dbContext.Customers.Remove(customer);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
