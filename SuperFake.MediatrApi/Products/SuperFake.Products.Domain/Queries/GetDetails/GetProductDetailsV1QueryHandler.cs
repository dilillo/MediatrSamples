using MediatR;
using SuperFake.Products.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Products.Domain
{
    public class GetProductDetailsV1QueryHandler : IRequestHandler<GetProductDetailsV1Query, Product>
    {
        private readonly SuperFakeProductsDbContext _dbContext;

        public GetProductDetailsV1QueryHandler(SuperFakeProductsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> Handle(GetProductDetailsV1Query request, CancellationToken cancellationToken)
        {
            return await _dbContext.Products.FindAsync(new object[] { request.ProductID }, cancellationToken);
        }
    }
}
