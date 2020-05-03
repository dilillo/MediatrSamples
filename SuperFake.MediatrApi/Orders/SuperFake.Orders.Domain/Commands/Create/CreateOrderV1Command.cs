using MediatR;
using SuperFake.Orders.Data;

namespace SuperFake.Orders.Domain
{
    public class CreateOrderV1Command : IRequest
    {
        public Order Order { get; set; }
    }
}
