using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class UpdateOrderItemOrderIsShippedAndCannotBeChangedException : DomainException
    {
        public UpdateOrderItemOrderIsShippedAndCannotBeChangedException(): base("Order is shipped and cannot be changed.")
        {
        }
    }
}