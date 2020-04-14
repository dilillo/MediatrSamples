using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class CreateOrderItemOrderItemAlreadyExistsForThatProductException : DomainException
    {
        public CreateOrderItemOrderItemAlreadyExistsForThatProductException() : base("Order item already exists for that product.")
        {
        }
    }
}