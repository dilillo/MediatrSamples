using SuperFake.Shared.Domain;

namespace SuperFake.Products.Domain
{
    public class DeleteProductDoesNotExistException : DomainException
    {
        public DeleteProductDoesNotExistException() : base("Product does not exist.")
        {
        }
    }
}
