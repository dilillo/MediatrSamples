using MediatR;
using SuperFake.Data;

namespace SuperFake.Domains
{
    public class UpdateOrderItemV1Command : IRequest
    {
        public OrderItem OrderItem { get; set; }
    }
}
