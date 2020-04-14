using MediatR;
using SuperFake.Customers.Data;

namespace SuperFake.Customers.Domain
{
    public class UpdateCustomerV1Command : IRequest
    {
        public Customer Customer { get; set; }
    }
}
