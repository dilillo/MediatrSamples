using MediatR;
using SuperFake.Data;

namespace SuperFake.Web
{
    public class GetOrderItemDetailsV1Query : IRequest<OrderItem>
    {
        public int OrderItemID { get; set; }
    }
}
