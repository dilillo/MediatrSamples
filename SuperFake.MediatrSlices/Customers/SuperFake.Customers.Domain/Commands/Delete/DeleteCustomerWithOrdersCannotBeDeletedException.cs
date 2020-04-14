using SuperFake.Shared.Domain;

namespace SuperFake.Customers.Domain
{
    public class DeleteCustomerWithOrdersCannotBeDeletedException : DomainException
    {
        public DeleteCustomerWithOrdersCannotBeDeletedException() : base("Customer with orders cannot be deleted.")
        {
        }
    }
}