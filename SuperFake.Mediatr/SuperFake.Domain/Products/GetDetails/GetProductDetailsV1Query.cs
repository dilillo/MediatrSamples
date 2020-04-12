using MediatR;
using SuperFake.Data;

namespace SuperFake.Domains
{
    public class GetProductDetailsV1Query : IRequest<Product>
    {
        public int ProductID { get; set; }
    }
}
