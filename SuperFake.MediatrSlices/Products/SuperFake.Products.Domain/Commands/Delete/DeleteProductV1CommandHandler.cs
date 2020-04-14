using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Products.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Products.Domain
{
    public class DeleteProductV1CommandHandler : IRequestHandler<DeleteProductV1Command>
    {
        private readonly SuperFakeProductsDbContext _dbContext;
        private readonly IMediator _mediator;

        public DeleteProductV1CommandHandler(SuperFakeProductsDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteProductV1Command request, CancellationToken cancellationToken)
        {
            await VerifyProductExists(request.ProductID);

            await VerifyProductHasNoOrders(request.ProductID);

            var product = await _dbContext.Products.FindAsync(request.ProductID);

            _dbContext.Products.Remove(product);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await PublishProductDeletedNotification(request.ProductID, cancellationToken);

            return Unit.Value;
        }

        private async Task PublishProductDeletedNotification(int productID, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new ProductDeletedV1Notification
            {
                ID = productID

            }, cancellationToken);
        }

        private async Task VerifyProductHasNoOrders(int productID)
        {
            var productHasOrders = await _dbContext.Products.AnyAsync(i => i.ID == productID && i.HasOrders == true);

            if (productHasOrders)
                throw new DeleteProductWithOrdersCannotBeDeletedException();
        }

        private async Task VerifyProductExists(int productID)
        {
            var productExists = await _dbContext.Products.AnyAsync(e => e.ID == productID);

            if (!productExists)
                throw new DeleteProductDoesNotExistException();
        }
    }
}
