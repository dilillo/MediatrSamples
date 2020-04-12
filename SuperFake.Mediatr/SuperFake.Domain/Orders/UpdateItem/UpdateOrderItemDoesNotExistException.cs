namespace SuperFake.Domains
{
    public class UpdateOrderItemDoesNotExistException : DomainException
    {
        public UpdateOrderItemDoesNotExistException() : base("Order item does not exist.")
        {
        }
    }
}