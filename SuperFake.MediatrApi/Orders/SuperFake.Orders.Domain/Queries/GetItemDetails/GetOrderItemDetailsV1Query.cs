using MediatR;
using SuperFake.Orders.Data;

namespace SuperFake.Orders.Domain
{
    public class GetOrderItemDetailsV1Query : IRequest<OrderItem>
    {
        public int OrderItemID { get; set; }
    }
}
