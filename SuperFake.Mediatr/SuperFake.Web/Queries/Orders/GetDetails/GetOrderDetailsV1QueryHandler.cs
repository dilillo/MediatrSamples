using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;


namespace SuperFake.Web
{
    public class GetOrderDetailsV1QueryHandler : IRequestHandler<GetOrderDetailsV1Query, Order>
    {
        private readonly SuperFakeDbContext _dbContext;

        public GetOrderDetailsV1QueryHandler(SuperFakeDbContext dbContext)
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
