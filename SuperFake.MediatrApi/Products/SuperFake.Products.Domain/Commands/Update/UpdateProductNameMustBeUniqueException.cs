using SuperFake.Shared.Domain;

namespace SuperFake.Products.Domain
{
    internal class UpdateProductNameMustBeUniqueException : DomainException
    {
        public UpdateProductNameMustBeUniqueException() : base("Product name must be unique.")
        {
        }
    }
}