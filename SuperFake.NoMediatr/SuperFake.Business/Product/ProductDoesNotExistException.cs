namespace SuperFake.Business
{
    public class ProductDoesNotExistException : BusinessException
    {
        public ProductDoesNotExistException() : base("Product does not exist.")
        {
        }
    }
}
