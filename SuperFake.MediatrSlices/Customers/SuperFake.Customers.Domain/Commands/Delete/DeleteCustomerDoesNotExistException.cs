using SuperFake.Shared.Domain;

namespace SuperFake.Customers.Domain
{
    public class DeleteCustomerDoesNotExistException : DomainException
    {
        public DeleteCustomerDoesNotExistException() : base("Customer does not exist.")
        {
        }
    }
}