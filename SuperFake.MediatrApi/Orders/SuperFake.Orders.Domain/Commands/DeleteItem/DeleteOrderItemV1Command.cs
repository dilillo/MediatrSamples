using MediatR;

namespace SuperFake.Orders.Domain
{
    public class DeleteOrderItemV1Command : IRequest
    {
        public int OrderItemID { get; set; }
    }
}
