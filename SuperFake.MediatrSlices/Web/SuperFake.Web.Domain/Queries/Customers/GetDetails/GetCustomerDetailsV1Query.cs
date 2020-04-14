using MediatR;
using SuperFake.Web.Data;

namespace SuperFake.Web.Domain
{
    public class GetCustomerDetailsV1Query : IRequest<Customer>
    {
        public int CustomerID { get; set; }
    }
}
