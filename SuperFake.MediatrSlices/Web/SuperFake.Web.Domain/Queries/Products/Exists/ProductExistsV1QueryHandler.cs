using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class ProductExistsV1QueryHandler : IRequestHandler<ProductExistsV1Query, bool>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public ProductExistsV1QueryHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> Handle(ProductExistsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Products.AnyAsync(e => e.ID == request.ProductID, cancellationToken);
        }
    }
}
