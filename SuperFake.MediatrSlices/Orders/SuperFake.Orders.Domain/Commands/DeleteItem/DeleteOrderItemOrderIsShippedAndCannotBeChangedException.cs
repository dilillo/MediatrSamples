using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class DeleteOrderItemOrderIsShippedAndCannotBeChangedException : DomainException
    {
        public DeleteOrderItemOrderIsShippedAndCannotBeChangedException(): base("Order is shipped and cannot be changed.")
        {
        }
    }
}