using SuperFake.Shared.Domain;

namespace SuperFake.Customers.Domain
{
    public class UpdateCustomerDoesNotExistException : DomainException
    {
        public UpdateCustomerDoesNotExistException() : base("Customer does not exist.")
        {
        }
    }
}