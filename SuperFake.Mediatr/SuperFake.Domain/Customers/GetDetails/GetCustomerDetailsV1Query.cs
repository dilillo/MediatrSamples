using MediatR;
using SuperFake.Data;

namespace SuperFake.Domains
{
    public class GetCustomerDetailsV1Query : IRequest<Customer>
    {
        public int CustomerID { get; set; }
    }
}
