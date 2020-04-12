using MediatR;
using SuperFake.Data;

namespace SuperFake.Web
{
    public class GetProductDetailsV1Query : IRequest<Product>
    {
        public int ProductID { get; set; }
    }
}
