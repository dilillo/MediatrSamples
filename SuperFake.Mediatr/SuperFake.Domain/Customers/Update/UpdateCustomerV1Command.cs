using MediatR;
using SuperFake.Data;

namespace SuperFake.Domains
{
    public class UpdateCustomerV1Command : IRequest
    {
        public Customer Customer { get; set; }
    }
}
