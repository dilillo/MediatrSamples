using MediatR;
using SuperFake.Data;

namespace SuperFake.Web
{
    public class GetOrderDetailsV1Query : IRequest<Order>
    {
        public int OrderID { get; set; }
    }
}
