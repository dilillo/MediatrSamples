using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class ProductCreatedV1NotificationHandler : INotificationHandler<ProductCreatedV1Notification>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public ProductCreatedV1NotificationHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(ProductCreatedV1Notification notification, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.SingleOrDefaultAsync(i => i.ID == notification.ID, cancellationToken);

            if (product != null)
                return;

            _dbContext.Products.Add(new Product 
            { 
                ID = notification.ID,
                Category = notification.Category,
                Description = notification.Description,
                Name = notification.Name,
                Price = notification.Price
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
