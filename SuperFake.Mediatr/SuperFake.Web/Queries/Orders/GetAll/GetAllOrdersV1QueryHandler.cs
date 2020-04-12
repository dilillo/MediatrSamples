using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web
{
    public class GetAllOrdersV1QueryHandler : IRequestHandler<GetAllOrdersV1Query, List<Order>>
    {
        private readonly SuperFakeDbContext _dbContext;

        public GetAllOrdersV1QueryHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Order>> Handle(GetAllOrdersV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Orders
                .Include(i => i.Customer)
                .Include(i => i.OrderItems)
                    .ThenInclude(i => i.Product)
                .ToListAsync();
        }
    }
}
