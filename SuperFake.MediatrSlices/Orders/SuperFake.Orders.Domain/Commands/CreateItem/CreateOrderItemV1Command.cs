using MediatR;
using SuperFake.Orders.Data;

namespace SuperFake.Orders.Domain
{
    public class CreateOrderItemV1Command : IRequest
    {
        public OrderItem OrderItem { get; set; }
    }
}
