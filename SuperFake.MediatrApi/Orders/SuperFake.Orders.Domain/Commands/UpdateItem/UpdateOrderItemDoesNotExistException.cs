using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class UpdateOrderItemDoesNotExistException : DomainException
    {
        public UpdateOrderItemDoesNotExistException() : base("Order item does not exist.")
        {
        }
    }
}