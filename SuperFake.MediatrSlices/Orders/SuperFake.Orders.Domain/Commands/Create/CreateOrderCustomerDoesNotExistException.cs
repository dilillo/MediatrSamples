using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class CreateOrderCustomerDoesNotExistException : DomainException
    {
        public CreateOrderCustomerDoesNotExistException() : base("Customer does not exist.")
        {
        }
    }
}