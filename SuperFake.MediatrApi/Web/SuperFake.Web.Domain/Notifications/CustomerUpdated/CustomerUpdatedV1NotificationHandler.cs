using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class CustomerUpdatedV1NotificationHandler : INotificationHandler<CustomerUpdatedV1Notification>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public CustomerUpdatedV1NotificationHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CustomerUpdatedV1Notification notification, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers.SingleOrDefaultAsync(i => i.ID == notification.ID, cancellationToken);

            if (customer == null)
            {
                customer = new Customer { ID = customer.ID };

                _dbContext.Customers.Add(customer);
            }

            customer.FirstName = notification.FirstName;
            customer.LastName = notification.LastName;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
