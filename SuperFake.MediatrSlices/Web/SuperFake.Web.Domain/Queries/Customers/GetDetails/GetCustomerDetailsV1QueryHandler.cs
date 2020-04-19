using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class GetCustomerDetailsV1QueryHandler : IRequestHandler<GetCustomerDetailsV1Query, Customer>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public GetCustomerDetailsV1QueryHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Customer> Handle(GetCustomerDetailsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Customers
                .Include(i => i.Orders)
                    .ThenInclude(i => i.OrderItems)
                        .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(i => i.ID == request.CustomerID, cancellationToken);
        }
    }
}
