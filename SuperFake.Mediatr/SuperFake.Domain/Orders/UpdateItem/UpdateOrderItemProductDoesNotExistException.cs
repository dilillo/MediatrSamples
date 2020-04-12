namespace SuperFake.Domains
{
    public class UpdateOrderItemProductDoesNotExistException : DomainException
    {
        public UpdateOrderItemProductDoesNotExistException() : base("Product does not exist.")
        {
        }
    }
}
