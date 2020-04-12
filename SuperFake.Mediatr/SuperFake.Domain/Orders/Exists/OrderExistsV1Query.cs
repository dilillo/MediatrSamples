using MediatR;

namespace SuperFake.Domains
{
    public class OrderExistsV1Query : IRequest<bool>
    {
        public int OrderID { get; set; }
    }
}
