namespace SuperFake.Domains
{
    public class UpdateOrderIsShippedAndCannotBeChangedException : DomainException
    {
        public UpdateOrderIsShippedAndCannotBeChangedException(): base("Order is shipped and cannot be changed.")
        {
        }
    }
}