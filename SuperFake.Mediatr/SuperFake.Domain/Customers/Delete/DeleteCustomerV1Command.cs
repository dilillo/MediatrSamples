using MediatR;

namespace SuperFake.Domains
{
    public class DeleteCustomerV1Command : IRequest
    {
        public int CustomerID { get; set; }
    }
}
