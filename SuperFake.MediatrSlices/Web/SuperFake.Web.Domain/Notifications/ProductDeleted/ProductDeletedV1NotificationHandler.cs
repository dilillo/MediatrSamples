using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Orders.Domain
{
    public class ProductDeletedV1NotificationHandler : INotificationHandler<ProductDeletedV1Notification>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public ProductDeletedV1NotificationHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(ProductDeletedV1Notification notification, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.SingleOrDefaultAsync(i => i.ID == notification.ID, cancellationToken);

            if (product == null)
                return;

            _dbContext.Products.Remove(product);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
