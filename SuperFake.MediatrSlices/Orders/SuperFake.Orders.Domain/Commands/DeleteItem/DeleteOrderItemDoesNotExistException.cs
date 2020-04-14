using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class DeleteOrderItemDoesNotExistException : DomainException
    {
        public DeleteOrderItemDoesNotExistException() : base("Order item does not exist.")
        {
        }
    }
}