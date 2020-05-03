using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class GetProductDetailsV1QueryHandler : IRequestHandler<GetProductDetailsV1Query, GetProductDetailsV1QueryResult>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public GetProductDetailsV1QueryHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetProductDetailsV1QueryResult> Handle(GetProductDetailsV1Query request, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .Select(i => new GetProductDetailsV1QueryResult
                {
                    Category = i.Category,
                    Description = i.Description,
                    ID = i.ID,
                    Name = i.Name,
                    Price = i.Price
                }).SingleOrDefaultAsync(i => i.ID == request.ProductID , cancellationToken);
        }
    }
}
