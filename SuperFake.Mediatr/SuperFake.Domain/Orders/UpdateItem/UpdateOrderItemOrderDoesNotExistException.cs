namespace SuperFake.Domains
{
    public class UpdateOrderItemOrderDoesNotExistException : DomainException
    {
        public UpdateOrderItemOrderDoesNotExistException() : base("Order does not exist.")
        {
        }
    }
}