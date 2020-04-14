using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class UpdateOrderIsShippedAndCannotBeChangedException : DomainException
    {
        public UpdateOrderIsShippedAndCannotBeChangedException(): base("Order is shipped and cannot be changed.")
        {
        }
    }
}