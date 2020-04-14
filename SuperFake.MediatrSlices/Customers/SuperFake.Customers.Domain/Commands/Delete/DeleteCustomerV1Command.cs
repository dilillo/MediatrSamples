using MediatR;

namespace SuperFake.Customers.Domain
{
    public class DeleteCustomerV1Command : IRequest
    {
        public int CustomerID { get; set; }
    }
}
