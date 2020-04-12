using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web
{
    public class CustomerExistsV1QueryHandler : IRequestHandler<CustomerExistsV1Query, bool>
    {
        private readonly SuperFakeDbContext _dbContext;

        public CustomerExistsV1QueryHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> Handle(CustomerExistsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Customers.AnyAsync(e => e.ID == request.CustomerID);
        }
    }
}
