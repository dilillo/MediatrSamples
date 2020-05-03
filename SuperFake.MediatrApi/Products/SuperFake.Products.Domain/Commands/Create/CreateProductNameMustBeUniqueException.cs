using SuperFake.Shared.Domain;

namespace SuperFake.Products.Domain
{
    internal class CreateProductNameMustBeUniqueException : DomainException
    {
        public CreateProductNameMustBeUniqueException() : base("Product name must be unique.")
        {
        }
    }
}