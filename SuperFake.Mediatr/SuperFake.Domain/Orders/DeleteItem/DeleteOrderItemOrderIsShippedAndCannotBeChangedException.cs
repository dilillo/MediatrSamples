namespace SuperFake.Domains
{
    public class DeleteOrderItemOrderIsShippedAndCannotBeChangedException : DomainException
    {
        public DeleteOrderItemOrderIsShippedAndCannotBeChangedException(): base("Order is shipped and cannot be changed.")
        {
        }
    }
}