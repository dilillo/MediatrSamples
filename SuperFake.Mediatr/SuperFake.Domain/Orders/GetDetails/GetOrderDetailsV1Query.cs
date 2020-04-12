using MediatR;
using SuperFake.Data;

namespace SuperFake.Domains
{
    public class GetOrderDetailsV1Query : IRequest<Order>
    {
        public int OrderID { get; set; }
    }
}
