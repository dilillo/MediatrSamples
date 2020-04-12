namespace SuperFake.Domains
{
    public class DeleteOrderItemOrderDoesNotExistException : DomainException
    {
        public DeleteOrderItemOrderDoesNotExistException() : base("Order does not exist.")
        {
        }
    }
}