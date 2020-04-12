namespace SuperFake.Domains
{
    public class CreateOrderItemOrderItemAlreadyExistsForThatProductException : DomainException
    {
        public CreateOrderItemOrderItemAlreadyExistsForThatProductException() : base("Order item already exists for that product.")
        {
        }
    }
}