using MediatR;
using SuperFake.Products.Data;

namespace SuperFake.Products.Domain
{
    public class UpdateProductV1Command : IRequest
    {
        public Product Product { get; set; }
    }
}
