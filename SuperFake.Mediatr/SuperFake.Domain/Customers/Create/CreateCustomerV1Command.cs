using MediatR;
using SuperFake.Data;

namespace SuperFake.Domains
{
    public class CreateCustomerV1Command : IRequest
    {
        public Customer Customer { get; set; }
    }
}
