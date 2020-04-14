using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class DeleteOrderDoesNotExistException : DomainException
    {
        public DeleteOrderDoesNotExistException() : base("Order does not exist.")
        {
        }
    }
}