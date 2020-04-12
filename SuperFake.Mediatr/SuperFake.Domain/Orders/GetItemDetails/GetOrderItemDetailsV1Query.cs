using MediatR;
using SuperFake.Data;

namespace SuperFake.Domains
{
    public class GetOrderItemDetailsV1Query : IRequest<OrderItem>
    {
        public int OrderItemID { get; set; }
    }
}
