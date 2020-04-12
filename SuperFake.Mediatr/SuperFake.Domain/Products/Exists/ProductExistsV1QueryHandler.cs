using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Domains
{
    public class ProductExistsV1QueryHandler : IRequestHandler<ProductExistsV1Query, bool>
    {
        private readonly SuperFakeDbContext _dbContext;

        public ProductExistsV1QueryHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> Handle(ProductExistsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Products.AnyAsync(e => e.ID == request.ProductID);
        }
    }
}
