namespace SuperFake.Domains
{
    public class CreateOrderItemOrderIsShippedAndCannotBeChangedException : DomainException
    {
        public CreateOrderItemOrderIsShippedAndCannotBeChangedException(): base("Order is shipped and cannot be changed.")
        {
        }
    }
}