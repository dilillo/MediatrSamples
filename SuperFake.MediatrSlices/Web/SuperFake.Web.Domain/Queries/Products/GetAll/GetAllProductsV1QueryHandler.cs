using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class GetAllProductsV1QueryHandler : IRequestHandler<GetAllProductsV1Query, List<Product>>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public GetAllProductsV1QueryHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Product>> Handle(GetAllProductsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Products.ToListAsync();
        }
    }
}
