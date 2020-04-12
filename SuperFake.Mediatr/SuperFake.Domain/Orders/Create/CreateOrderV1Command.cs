using MediatR;
using SuperFake.Data;

namespace SuperFake.Domains
{
    public class CreateOrderV1Command : IRequest
    {
        public Order Order { get; set; }
    }
}
