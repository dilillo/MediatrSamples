namespace SuperFake.Domains
{
    internal class CreateProductNameMustBeUniqueException : DomainException
    {
        public CreateProductNameMustBeUniqueException() : base("Product name must be unique.")
        {
        }
    }
}