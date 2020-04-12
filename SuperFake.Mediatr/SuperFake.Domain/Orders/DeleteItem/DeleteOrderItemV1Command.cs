using MediatR;

namespace SuperFake.Domains
{
    public class DeleteOrderItemV1Command : IRequest
    {
        public int OrderItemID { get; set; }
    }
}
