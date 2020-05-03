using MediatR;
using SuperFake.Customers.Data;

namespace SuperFake.Customers.Domain
{
    public class CreateCustomerV1Command : IRequest
    {
        public Customer Customer { get; set; }
    }
}
