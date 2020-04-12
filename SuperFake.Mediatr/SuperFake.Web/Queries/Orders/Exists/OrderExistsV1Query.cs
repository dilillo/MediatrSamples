using MediatR;

namespace SuperFake.Web
{
    public class OrderExistsV1Query : IRequest<bool>
    {
        public int OrderID { get; set; }
    }
}
