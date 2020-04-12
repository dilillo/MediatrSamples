using MediatR;

namespace SuperFake.Domains
{
    public class DeleteProductV1Command :  IRequest
    {
        public int ProductID { get; set; }
    }
}
