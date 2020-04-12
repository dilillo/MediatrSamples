using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;


namespace SuperFake.Web
{
    public class GetOrderItemDetailsV1QueryHandler : IRequestHandler<GetOrderItemDetailsV1Query, OrderItem>
    {
        private readonly SuperFakeDbContext _dbContext;

        public GetOrderItemDetailsV1QueryHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<OrderItem> Handle(GetOrderItemDetailsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.OrderItems
                .Include(i => i.Product)
                .Include(i => i.Order)
                .FirstOrDefaultAsync(i => i.ID == request.OrderItemID);
        }
    }
}
