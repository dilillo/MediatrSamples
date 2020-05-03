using MediatR;
using SuperFake.Orders.Data;

namespace SuperFake.Orders.Domain
{
    public class GetOrderDetailsV1Query : IRequest<Order>
    {
        public int OrderID { get; set; }
    }
}
