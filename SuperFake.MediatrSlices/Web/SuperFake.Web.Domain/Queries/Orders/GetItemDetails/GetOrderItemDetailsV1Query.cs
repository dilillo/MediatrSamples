using MediatR;
using SuperFake.Web.Data;

namespace SuperFake.Web.Domain
{
    public class GetOrderItemDetailsV1Query : IRequest<OrderItem>
    {
        public int OrderItemID { get; set; }
    }
}
