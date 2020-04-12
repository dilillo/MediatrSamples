using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web
{
    public class GetAllCustomersV1QueryHandler : IRequestHandler<GetAllCustomersV1Query, List<Customer>>
    {
        private readonly SuperFakeDbContext _dbContext;

        public GetAllCustomersV1QueryHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Customer>> Handle(GetAllCustomersV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Customers
                .Include(i => i.Orders)
                .ToListAsync();
        }
    }
}
