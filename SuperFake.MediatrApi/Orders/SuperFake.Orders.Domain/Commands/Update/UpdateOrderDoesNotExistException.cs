using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class UpdateOrderDoesNotExistException : DomainException
    {
        public UpdateOrderDoesNotExistException() : base("Order does not exist.")
        {
        }
    }
}