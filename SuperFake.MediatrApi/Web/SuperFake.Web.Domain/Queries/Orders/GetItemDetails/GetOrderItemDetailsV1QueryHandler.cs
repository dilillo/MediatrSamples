using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class GetOrderItemDetailsV1QueryHandler : IRequestHandler<GetOrderItemDetailsV1Query, GetOrderItemDetailsV1QueryResult>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public GetOrderItemDetailsV1QueryHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<GetOrderItemDetailsV1QueryResult> Handle(GetOrderItemDetailsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.OrderItems
                .Select(i => new GetOrderItemDetailsV1QueryResult
                {
                    ID = i.ID,
                    OrderID = i.OrderID,
                    ProductID = i.ProductID,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    TotalPrice = i.TotalPrice
                })
                .FirstOrDefaultAsync(i => i.ID == request.OrderItemID, cancellationToken);
        }
    }
}
