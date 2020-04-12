using MediatR;

namespace SuperFake.Domains
{
    public class CustomerExistsV1Query : IRequest<bool>
    {
        public int CustomerID { get; set; }
    }
}
