using MediatR;

namespace SuperFake.Products.Domain
{
    public class DeleteProductV1Command :  IRequest
    {
        public int ProductID { get; set; }
    }
}
