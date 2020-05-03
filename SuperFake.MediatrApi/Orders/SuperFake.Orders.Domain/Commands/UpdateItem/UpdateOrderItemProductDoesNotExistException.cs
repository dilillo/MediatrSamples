using SuperFake.Shared.Domain;

namespace SuperFake.Orders.Domain
{
    public class UpdateOrderItemProductDoesNotExistException : DomainException
    {
        public UpdateOrderItemProductDoesNotExistException() : base("Product does not exist.")
        {
        }
    }
}
