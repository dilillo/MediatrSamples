using MediatR;
using SuperFake.Products.Data;

namespace SuperFake.Products.Domain
{
    public class CreateProductV1Command : IRequest
    {
        public Product Product { get; set; }
    }
}
