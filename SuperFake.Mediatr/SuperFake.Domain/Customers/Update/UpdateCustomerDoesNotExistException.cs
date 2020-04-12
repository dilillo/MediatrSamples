namespace SuperFake.Domains
{
    public class UpdateCustomerDoesNotExistException : DomainException
    {
        public UpdateCustomerDoesNotExistException() : base("Customer does not exist.")
        {
        }
    }
}