namespace SuperFake.Domains
{
    public class CreateOrderCustomerDoesNotExistException : DomainException
    {
        public CreateOrderCustomerDoesNotExistException() : base("Customer does not exist.")
        {
        }
    }
}