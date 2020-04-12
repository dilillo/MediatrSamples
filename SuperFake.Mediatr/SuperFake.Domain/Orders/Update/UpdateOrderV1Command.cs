using MediatR;
using SuperFake.Data;

namespace SuperFake.Domains
{
    public class UpdateOrderV1Command : IRequest
    {
        public Order Order { get; set; }
    }
}
