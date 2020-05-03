using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class GetAllProductsV1QueryHandler : IRequestHandler<GetAllProductsV1Query, List<GetAllProductsV1QueryResult>>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public GetAllProductsV1QueryHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<GetAllProductsV1QueryResult>> Handle(GetAllProductsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Products
                .Select(i => new GetAllProductsV1QueryResult
                {
                    Category = i.Category,
                    Description = i.Description,
                    ID = i.ID,
                    Name = i.Name,
                    Price = i.Price
                }).ToListAsync(cancellationToken);
        }
    }
}
