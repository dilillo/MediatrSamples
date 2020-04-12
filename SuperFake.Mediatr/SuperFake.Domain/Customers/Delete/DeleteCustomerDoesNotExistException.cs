namespace SuperFake.Domains
{
    public class DeleteCustomerDoesNotExistException : DomainException
    {
        public DeleteCustomerDoesNotExistException() : base("Customer does not exist.")
        {
        }
    }
}