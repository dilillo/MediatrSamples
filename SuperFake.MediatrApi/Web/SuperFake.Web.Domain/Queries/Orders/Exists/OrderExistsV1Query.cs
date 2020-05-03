using MediatR;

namespace SuperFake.Web.Domain
{
    public class OrderExistsV1Query : IRequest<bool>
    {
        public int OrderID { get; set; }
    }
}
