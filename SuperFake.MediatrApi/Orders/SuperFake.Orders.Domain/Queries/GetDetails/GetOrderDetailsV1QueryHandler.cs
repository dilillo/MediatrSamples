using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Orders.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Orders.Domain
{
    public class GetOrderDetailsV1QueryHandler : IRequestHandler<GetOrderDetailsV1Query, Order>
    {
        private readonly SuperFakeOrdersDbContext _dbContext;

        public GetOrderDetailsV1QueryHandler(SuperFakeOrdersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Order> Handle(GetOrderDetailsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Orders
                 .Include(i => i.OrderItems)
                 .FirstOrDefaultAsync(i => i.ID == request.OrderID, cancellationToken);
        }
    }
}
