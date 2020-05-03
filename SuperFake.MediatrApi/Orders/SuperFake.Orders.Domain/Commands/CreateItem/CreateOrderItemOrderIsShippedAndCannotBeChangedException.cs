using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class CreateOrderItemOrderIsShippedAndCannotBeChangedException : DomainException
    {
        public CreateOrderItemOrderIsShippedAndCannotBeChangedException(): base("Order is shipped and cannot be changed.")
        {
        }
    }
}