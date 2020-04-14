using MediatR;
using SuperFake.Orders.Data;

namespace SuperFake.Orders.Domain
{
    public class UpdateOrderItemV1Command : IRequest
    {
        public OrderItem OrderItem { get; set; }
    }
}
