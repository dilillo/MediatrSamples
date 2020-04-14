using MediatR;
using SuperFake.Web.Data;

namespace SuperFake.Web.Domain
{
    public class GetOrderDetailsV1Query : IRequest<Order>
    {
        public int OrderID { get; set; }
    }
}
