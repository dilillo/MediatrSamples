using MediatR;
using SuperFake.Web.Data;
using System.Collections.Generic;

namespace SuperFake.Web.Domain
{
    public class GetAllOrdersV1Query : IRequest<List<Order>>
    {
    }
}
