using MediatR;
using System.Collections.Generic;

namespace SuperFake.Web.Domain
{
    public class GetAllOrdersV1Query : IRequest<List<GetAllOrdersV1QueryResult>>
    {
    }
}
