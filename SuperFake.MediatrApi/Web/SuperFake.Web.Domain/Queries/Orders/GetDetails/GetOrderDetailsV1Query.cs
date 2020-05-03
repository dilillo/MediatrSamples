using MediatR;

namespace SuperFake.Web.Domain
{
    public class GetOrderDetailsV1Query : IRequest<GetOrderDetailsV1QueryResult>
    {
        public int OrderID { get; set; }
    }
}
