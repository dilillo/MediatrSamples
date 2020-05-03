using MediatR;

namespace SuperFake.Orders.Domain
{
    public class DeleteOrderV1Command : IRequest
    {
        public int OrderID { get; set; }
    }
}
