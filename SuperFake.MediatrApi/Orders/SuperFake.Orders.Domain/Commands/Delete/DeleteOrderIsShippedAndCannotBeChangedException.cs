using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class DeleteOrderIsShippedAndCannotBeChangedException : DomainException
    {
        public DeleteOrderIsShippedAndCannotBeChangedException(): base("Order is shipped and cannot be changed.")
        {
        }
    }
}