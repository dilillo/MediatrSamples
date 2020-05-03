using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class CreateOrderItemOrderDoesNotExistException : DomainException
    {
        public CreateOrderItemOrderDoesNotExistException() : base("Order does not exist.")
        {
        }
    }
}