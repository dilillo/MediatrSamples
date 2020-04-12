namespace SuperFake.Domains
{
    public class CreateOrderItemProductDoesNotExistException : DomainException
    {
        public CreateOrderItemProductDoesNotExistException() : base("Product does not exist.")
        {
        }
    }
}
