using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Domains
{
    public class DeleteProductV1CommandHandler : IRequestHandler<DeleteProductV1Command>
    {
        private readonly SuperFakeDbContext _dbContext;

        public DeleteProductV1CommandHandler(SuperFakeDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteProductV1Command request, CancellationToken cancellationToken)
        {
            await VerifyProductExists(request.ProductID);

            await VerifyProductHasNoOrders( request.ProductID);

            var product = await _dbContext.Products.FindAsync(request.ProductID);

            _dbContext.Products.Remove(product);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task VerifyProductHasNoOrders(int productID)
        {
            var productHasOrders = await _dbContext.OrderItems.AnyAsync(i => i.ProductID == productID);

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
