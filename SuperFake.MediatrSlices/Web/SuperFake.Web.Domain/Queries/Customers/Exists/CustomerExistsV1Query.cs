using MediatR;

namespace SuperFake.Web.Domain
{
    public class CustomerExistsV1Query : IRequest<bool>
    {
        public int CustomerID { get; set; }
    }
}
