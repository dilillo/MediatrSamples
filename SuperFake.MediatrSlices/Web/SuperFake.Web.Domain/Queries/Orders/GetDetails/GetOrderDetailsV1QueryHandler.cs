using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using System.Threading;
using System.Threading.Tasks;


namespace SuperFake.Web.Domain
{
    public class GetOrderDetailsV1QueryHandler : IRequestHandler<GetOrderDetailsV1Query, Order>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public GetOrderDetailsV1QueryHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Order> Handle(GetOrderDetailsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Orders
                 .Include(i => i.Customer)
                 .Include(i => i.OrderItems)
                     .ThenInclude(i2 => i2.Product)
                 .FirstOrDefaultAsync(i => i.ID == request.OrderID);
        }
    }
}
