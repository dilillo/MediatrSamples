using MediatR;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web
{
    public class GetProductDetailsV1QueryHandler : IRequestHandler<GetProductDetailsV1Query, Product>
    {
        private readonly SuperFakeDbContext _dbContext;

        public GetProductDetailsV1QueryHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> Handle(GetProductDetailsV1Query request, CancellationToken cancellationToken)
        {
            return await _dbContext.Products.FindAsync(request.ProductID);
        }
    }
}
