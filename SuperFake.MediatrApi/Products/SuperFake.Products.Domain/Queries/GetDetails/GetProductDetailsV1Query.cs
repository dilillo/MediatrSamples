using MediatR;
using SuperFake.Products.Data;

namespace SuperFake.Products.Domain
{
    public class GetProductDetailsV1Query : IRequest<Product>
    {
        public int ProductID { get; set; }
    }
}
