using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Orders.Data;
using System.Threading;
using System.Threading.Tasks;


namespace SuperFake.Orders.Domain
{
    public class GetOrderItemDetailsV1QueryHandler : IRequestHandler<GetOrderItemDetailsV1Query, OrderItem>
    {
        private readonly SuperFakeOrdersDbContext _dbContext;

        public GetOrderItemDetailsV1QueryHandler(SuperFakeOrdersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<OrderItem> Handle(GetOrderItemDetailsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.OrderItems
                .Include(i => i.Order)
                .FirstOrDefaultAsync(i => i.ID == request.OrderItemID, cancellationToken);
        }
    }
}
