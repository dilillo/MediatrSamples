namespace SuperFake.Domains
{
    public class CreateOrderItemOrderDoesNotExistException : DomainException
    {
        public CreateOrderItemOrderDoesNotExistException() : base("Order does not exist.")
        {
        }
    }
}