using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Domains
{
    public class UpdateProductV1CommandHandler : IRequestHandler<UpdateProductV1Command>
    {
        private readonly SuperFakeDbContext _dbContext;

        public UpdateProductV1CommandHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateProductV1Command request, CancellationToken cancellationToken)
        {
            await VerifyProductExists(request.Product.ID);

            await VerifyProductNameIsUnique(request.Product.ID, request.Product.Name);

            _dbContext.Update(request.Product);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task VerifyProductNameIsUnique(int productID, string productName)
        {
            var nameExists = await _dbContext.Products.AnyAsync(i => i.ID != productID && i.Name == productName);

            if (nameExists)
                throw new UpdateProductNameMustBeUniqueException();
        }

        private async Task VerifyProductExists(int productID)
        {
            var productExists = await _dbContext.Products.AnyAsync(e => e.ID == productID);

            if (!productExists)
                throw new UpdateProductDoesNotExistException();
        }
    }
}
