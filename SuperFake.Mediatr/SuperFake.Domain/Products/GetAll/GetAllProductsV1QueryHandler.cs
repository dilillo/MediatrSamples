using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Domains
{
    public class GetAllProductsV1QueryHandler : IRequestHandler<GetAllProductsV1Query, List<Product>>
    {
        private readonly SuperFakeDbContext _dbContext;

        public GetAllProductsV1QueryHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Product>> Handle(GetAllProductsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Products.ToListAsync();
        }
    }
}
