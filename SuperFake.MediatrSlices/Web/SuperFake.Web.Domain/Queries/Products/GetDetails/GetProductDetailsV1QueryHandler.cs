using MediatR;
using SuperFake.Web.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class GetProductDetailsV1QueryHandler : IRequestHandler<GetProductDetailsV1Query, Product>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public GetProductDetailsV1QueryHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> Handle(GetProductDetailsV1Query request, CancellationToken cancellationToken)
        {
            return await _dbContext.Products.FindAsync(request.ProductID);
        }
    }
}
