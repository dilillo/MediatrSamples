namespace SuperFake.Business
{
    internal class ProductNameMustBeUniqueException : BusinessException
    {
        public ProductNameMustBeUniqueException() : base("Product name must be unique.")
        {
        }
    }
}