using SuperFake.Shared.Domain;

namespace SuperFake.Customers.Domain
{
    public class UpdateCustomerNameMustBeUniqueException : DomainException
    {
        public UpdateCustomerNameMustBeUniqueException() : base("Customer name must be unique.")
        {
        }
    }
}