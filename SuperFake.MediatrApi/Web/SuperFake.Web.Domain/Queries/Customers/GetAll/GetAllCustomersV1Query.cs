using MediatR;
using SuperFake.Web.Data;
using System.Collections.Generic;

namespace SuperFake.Web.Domain
{
    public class GetAllCustomersV1Query : IRequest<List<GetAllCustomersV1QueryResult>>
    {
    }
}
