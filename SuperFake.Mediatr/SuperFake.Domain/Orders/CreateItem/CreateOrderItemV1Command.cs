using MediatR;
using SuperFake.Data;

namespace SuperFake.Domains
{
    public class CreateOrderItemV1Command : IRequest
    {
        public OrderItem OrderItem { get; set; }
    }
}
