namespace SuperFake.Domains
{
    public class DeleteOrderItemDoesNotExistException : DomainException
    {
        public DeleteOrderItemDoesNotExistException() : base("Order item does not exist.")
        {
        }
    }
}