using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Products.Data;
using SuperFake.Shared.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Products.Domain
{
    public class OrderUpdatedV1NotificationHandler : INotificationHandler<OrderUpdatedV1Notification>
    {
        private readonly SuperFakeProductsDbContext _dbContext;

        public OrderUpdatedV1NotificationHandler(SuperFakeProductsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(OrderUpdatedV1Notification notification, CancellationToken cancellationToken)
        {
            var referencedProductIds = notification.OrderItems.Select(i => i.ProductID).ToArray();

            var products = await _dbContext.Products.Where(i => referencedProductIds.Contains(i.ID) && i.HasOrders == false).ToListAsync(cancellationToken);

            foreach (var product in products)
            {
                product.HasOrders = true;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
