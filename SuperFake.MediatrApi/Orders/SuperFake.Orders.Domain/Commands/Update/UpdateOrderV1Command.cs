using MediatR;
using SuperFake.Orders.Data;

namespace SuperFake.Orders.Domain
{
    public class UpdateOrderV1Command : IRequest
    {
        public Order Order { get; set; }
    }
}
