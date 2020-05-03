using MediatR;
using SuperFake.Web.Data;

namespace SuperFake.Web.Domain
{
    public class GetProductDetailsV1Query : IRequest<GetProductDetailsV1QueryResult>
    {
        public int ProductID { get; set; }
    }
}
