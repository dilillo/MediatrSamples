using MediatR;
using SuperFake.Web.Data;
using System.Collections.Generic;

namespace SuperFake.Web.Domain
{
    public class GetAllProductsV1Query : IRequest<List<Product>>
    {
    }
}
