using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Domains
{
    public class CreateProductV1CommandHandler : IRequestHandler<CreateProductV1Command>
    {
        private readonly SuperFakeDbContext _dbContext;

        public CreateProductV1CommandHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateProductV1Command request, CancellationToken cancellationToken)
        {
            await VerifyProductNameIsUnique( request.Product.Name);

            _dbContext.Products.Add(request.Product);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task VerifyProductNameIsUnique(string productName)
        {
            var nameExists = await _dbContext.Products.AnyAsync(i => i.Name == productName);

            if (nameExists)
                throw new CreateProductNameMustBeUniqueException();
        }
    }
}
