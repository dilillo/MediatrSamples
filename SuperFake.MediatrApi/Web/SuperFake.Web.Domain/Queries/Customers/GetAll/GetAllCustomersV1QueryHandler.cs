using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Web.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Domain
{
    public class GetAllCustomersV1QueryHandler : IRequestHandler<GetAllCustomersV1Query, List<GetAllCustomersV1QueryResult>>
    {
        private readonly SuperFakeWebDbContext _dbContext;

        public GetAllCustomersV1QueryHandler(SuperFakeWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<GetAllCustomersV1QueryResult>> Handle(GetAllCustomersV1Query request, CancellationToken cancellationToken)
        {
            return _dbContext.Customers
                .Select(i => new GetAllCustomersV1QueryResult
                {
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    FullName = i.FullName,
                    ID = i.ID
                })
                .ToListAsync(cancellationToken);
        }
    }
}
