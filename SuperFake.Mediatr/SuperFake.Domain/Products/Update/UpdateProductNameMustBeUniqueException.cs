namespace SuperFake.Domains
{
    internal class UpdateProductNameMustBeUniqueException : DomainException
    {
        public UpdateProductNameMustBeUniqueException() : base("Product name must be unique.")
        {
        }
    }
}