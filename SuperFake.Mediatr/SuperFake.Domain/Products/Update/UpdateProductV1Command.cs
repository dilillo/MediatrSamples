using MediatR;
using SuperFake.Data;

namespace SuperFake.Domains
{
    public class UpdateProductV1Command : IRequest
    {
        public Product Product { get; set; }
    }
}
