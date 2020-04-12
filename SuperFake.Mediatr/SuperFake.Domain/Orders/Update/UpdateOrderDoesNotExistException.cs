namespace SuperFake.Domains
{
    public class UpdateOrderDoesNotExistException : DomainException
    {
        public UpdateOrderDoesNotExistException() : base("Order does not exist.")
        {
        }
    }
}