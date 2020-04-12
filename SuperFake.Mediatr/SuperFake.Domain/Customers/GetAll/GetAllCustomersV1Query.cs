using MediatR;
using SuperFake.Data;
using System.Collections.Generic;

namespace SuperFake.Domains
{
    public class GetAllCustomersV1Query : IRequest<List<Customer>>
    {
    }
}
