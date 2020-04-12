using MediatR;
using SuperFake.Data;
using System.Collections.Generic;

namespace SuperFake.Domains
{
    public class GetAllOrdersV1Query : IRequest<List<Order>>
    {
    }
}
