using MediatR;

namespace SuperFake.Web.Domain
{
    public class GetOrderItemDetailsV1Query : IRequest<GetOrderItemDetailsV1QueryResult>
    {
        public int OrderItemID { get; set; }
    }
}
