namespace SuperFake.Domains
{
    public class DeleteOrderIsShippedAndCannotBeChangedException : DomainException
    {
        public DeleteOrderIsShippedAndCannotBeChangedException(): base("Order is shipped and cannot be changed.")
        {
        }
    }
}