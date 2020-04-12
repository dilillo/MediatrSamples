using MediatR;
using SuperFake.Data;
using System.Collections.Generic;

namespace SuperFake.Web
{
    public class GetAllProductsV1Query : IRequest<List<Product>>
    {
    }
}
