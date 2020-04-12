using MediatR;
using SuperFake.Data;

namespace SuperFake.Domains
{
    public class CreateProductV1Command : IRequest
    {
        public Product Product { get; set; }
    }
}
