using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class GetAllOrdersV1QueryHandler : IRequestHandler<GetAllOrdersV1Query, List<GetAllOrdersV1QueryResult>>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public GetAllOrdersV1QueryHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<GetAllOrdersV1QueryResult>> Handle(GetAllOrdersV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Orders
                .Select(i => new GetAllOrdersV1QueryResult
                {
                    CustomerID = i.CustomerID,
                    CustomerName = i.Customer.FullName,
                    ID = i.ID,
                    OrderDate = i.OrderDate,
                    OrderStatus = i.OrderStatus,
                    TotalPrice = i.TotalPrice
                })
                .ToListAsync(cancellationToken);
        }
    }
}
