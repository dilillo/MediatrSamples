using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Orders.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Orders.Domain
{
    public class ProductCreatedV1NotificationHandler : INotificationHandler<ProductCreatedV1Notification>
    {
        private readonly SuperFakeOrdersDbContext _dbContext;

        public ProductCreatedV1NotificationHandler(SuperFakeOrdersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(ProductCreatedV1Notification notification, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.SingleOrDefaultAsync(i => i.ID == notification.ID, cancellationToken);

            if (product != null)
                return;

            _dbContext.Products.Add(new Product { ID = notification.ID });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
