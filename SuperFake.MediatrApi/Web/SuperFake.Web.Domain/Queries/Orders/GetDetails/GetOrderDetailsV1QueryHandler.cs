using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class GetOrderDetailsV1QueryHandler : IRequestHandler<GetOrderDetailsV1Query, GetOrderDetailsV1QueryResult>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public GetOrderDetailsV1QueryHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<GetOrderDetailsV1QueryResult> Handle(GetOrderDetailsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Orders.Select(i => new GetOrderDetailsV1QueryResult
            {
                CustomerID = i.CustomerID,
                CustomerName = i.Customer.FullName,
                ID = i.ID,
                OrderDate = i.OrderDate,
                OrderStatus = i.OrderStatus,
                TotalPrice = i.TotalPrice,
                OrderItems = i.OrderItems.Select(i2 => new GetOrderDetailsV1QueryResultOrderItem
                { 
                    ID = i2.ID,
                    ProductID = i2.ProductID,
                    ProductName = i2.Product.Name,
                    Quantity = i2.Quantity,
                    TotalPrice = i2.TotalPrice
                })
            })
            .FirstOrDefaultAsync(i => i.ID == request.OrderID, cancellationToken);
        }
    }
}
