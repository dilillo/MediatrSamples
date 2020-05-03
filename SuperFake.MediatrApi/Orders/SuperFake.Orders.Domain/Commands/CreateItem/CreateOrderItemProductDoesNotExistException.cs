using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class CreateOrderItemProductDoesNotExistException : DomainException
    {
        public CreateOrderItemProductDoesNotExistException() : base("Product does not exist.")
        {
        }
    }
}
