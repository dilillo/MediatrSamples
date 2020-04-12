namespace SuperFake.Domains
{
    public class UpdateOrderItemOrderIsShippedAndCannotBeChangedException : DomainException
    {
        public UpdateOrderItemOrderIsShippedAndCannotBeChangedException(): base("Order is shipped and cannot be changed.")
        {
        }
    }
}