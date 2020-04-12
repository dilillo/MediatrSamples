using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Domains
{
    public class GetCustomerDetailsV1QueryHandler : IRequestHandler<GetCustomerDetailsV1Query, Customer>
    {
        private readonly SuperFakeDbContext _dbContext;

        public GetCustomerDetailsV1QueryHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Customer> Handle(GetCustomerDetailsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Customers
                .Include(i => i.Orders)
                    .ThenInclude(i => i.OrderItems)
                        .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(i => i.ID == request.CustomerID);
        }
    }
}
