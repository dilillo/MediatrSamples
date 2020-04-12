using MediatR;
using SuperFake.Data;
using System.Collections.Generic;

namespace SuperFake.Web
{
    public class GetAllOrdersV1Query : IRequest<List<Order>>
    {
    }
}
