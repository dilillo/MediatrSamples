using SuperFake.Shared.Domain;

namespace SuperFake.Customers.Domain
{
    public class CreateCustomerNameMustBeUniqueException : DomainException
    {
        public CreateCustomerNameMustBeUniqueException() : base("Customer name must be unique.")
        {
        }
    }
}