using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web
{
    public class OrderExistsV1QueryHandler : IRequestHandler<OrderExistsV1Query, bool>
    {
        private readonly SuperFakeDbContext _dbContext;

        public OrderExistsV1QueryHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> Handle(OrderExistsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Orders.AnyAsync(e => e.ID == request.OrderID);
        }
    }
}
