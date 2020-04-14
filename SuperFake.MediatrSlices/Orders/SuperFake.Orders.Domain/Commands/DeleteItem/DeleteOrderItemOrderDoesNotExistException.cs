using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class DeleteOrderItemOrderDoesNotExistException : DomainException
    {
        public DeleteOrderItemOrderDoesNotExistException() : base("Order does not exist.")
        {
        }
    }
}