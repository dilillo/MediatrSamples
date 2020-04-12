namespace SuperFake.Domains
{
    public class DeleteOrderDoesNotExistException : DomainException
    {
        public DeleteOrderDoesNotExistException() : base("Order does not exist.")
        {
        }
    }
}