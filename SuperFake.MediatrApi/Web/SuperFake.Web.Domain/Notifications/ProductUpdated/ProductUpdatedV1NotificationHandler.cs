using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class ProductUpdatedV1NotificationHandler : INotificationHandler<ProductUpdatedV1Notification>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public ProductUpdatedV1NotificationHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(ProductUpdatedV1Notification notification, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.SingleOrDefaultAsync(i => i.ID == notification.ID, cancellationToken);

            if (product == null)
            {
                product = new Product { ID = product.ID };

                _dbContext.Products.Add(product);
            }

            product.Category = notification.Category;
            product.Description = notification.Description;
            product.Name = notification.Name;
            product.Price = notification.Price;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
