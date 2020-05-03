using MediatR;
using SuperFake.Customers.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Customers.Domain
{
    public class GetCustomerDetailsV1QueryHandler : IRequestHandler<GetCustomerDetailsV1Query, Customer>
    {
        private readonly SuperFakeCustomersDbContext _dbContext;

        public GetCustomerDetailsV1QueryHandler(SuperFakeCustomersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> Handle(GetCustomerDetailsV1Query request, CancellationToken cancellationToken)
        {
            return await _dbContext.Customers
                .FindAsync(new object[] { request.CustomerID }, cancellationToken);
        }
    }
}
