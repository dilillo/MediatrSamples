using MediatR;

namespace SuperFake.Domains
{
    public class DeleteOrderV1Command : IRequest
    {
        public int OrderID { get; set; }
    }
}
