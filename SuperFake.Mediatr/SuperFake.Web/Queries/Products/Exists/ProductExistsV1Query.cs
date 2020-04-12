using MediatR;

namespace SuperFake.Web
{
    public class ProductExistsV1Query : IRequest<bool>
    {
        public int ProductID { get; set; }
    }
}
