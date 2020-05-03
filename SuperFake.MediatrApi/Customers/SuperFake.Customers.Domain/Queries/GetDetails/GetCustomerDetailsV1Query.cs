using MediatR;
using SuperFake.Customers.Data;

namespace SuperFake.Customers.Domain
{
    public class GetCustomerDetailsV1Query : IRequest<Customer>
    {
        public int CustomerID { get; set; }
    }
}
