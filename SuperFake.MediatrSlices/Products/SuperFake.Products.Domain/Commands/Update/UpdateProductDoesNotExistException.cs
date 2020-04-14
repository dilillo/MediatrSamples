using SuperFake.Shared.Domain;

namespace SuperFake.Products.Domain
{
    public class UpdateProductDoesNotExistException : DomainException
    {
        public UpdateProductDoesNotExistException() : base("Product does not exist.")
        {
        }
    }
}
