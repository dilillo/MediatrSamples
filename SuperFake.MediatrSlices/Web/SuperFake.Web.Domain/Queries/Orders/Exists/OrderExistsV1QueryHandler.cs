using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class OrderExistsV1QueryHandler : IRequestHandler<OrderExistsV1Query, bool>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public OrderExistsV1QueryHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> Handle(OrderExistsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Orders.AnyAsync(e => e.ID == request.OrderID, cancellationToken);
        }
    }
}
