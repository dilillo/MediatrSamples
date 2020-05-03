using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class GetCustomerDetailsV1QueryHandler : IRequestHandler<GetCustomerDetailsV1Query, GetCustomerDetailsV1QueryResult>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public GetCustomerDetailsV1QueryHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<GetCustomerDetailsV1QueryResult> Handle(GetCustomerDetailsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Customers
                .Select(i => new GetCustomerDetailsV1QueryResult
                {
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    FullName = i.FullName,
                    ID = i.ID,
                    Orders = i.Orders.Select(i2 => new GetCustomerDetailsV1QueryResultOrder
                    {
                        ID = i2.ID,
                        OrderDate = i2.OrderDate,
                        OrderStatus = i2.OrderStatus,
                        TotalPrice = i2.TotalPrice
                    })
                })
                .FirstOrDefaultAsync(i => i.ID == request.CustomerID, cancellationToken);
        }
    }
}
