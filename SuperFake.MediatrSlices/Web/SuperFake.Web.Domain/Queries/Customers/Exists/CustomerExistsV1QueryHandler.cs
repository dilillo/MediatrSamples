using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class CustomerExistsV1QueryHandler : IRequestHandler<CustomerExistsV1Query, bool>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public CustomerExistsV1QueryHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> Handle(CustomerExistsV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Customers.AnyAsync(e => e.ID == request.CustomerID);
        }
    }
}
