using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class UpdateOrderItemOrderDoesNotExistException : DomainException
    {
        public UpdateOrderItemOrderDoesNotExistException() : base("Order does not exist.")
        {
        }
    }
}