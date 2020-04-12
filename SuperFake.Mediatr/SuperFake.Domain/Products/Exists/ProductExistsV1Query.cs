using MediatR;

namespace SuperFake.Domains
{
    public class ProductExistsV1Query : IRequest<bool>
    {
        public int ProductID { get; set; }
    }
}
