using MediatR;

namespace SuperFake.Web.Domain
{
    public class ProductExistsV1Query : IRequest<bool>
    {
        public int ProductID { get; set; }
    }
}
