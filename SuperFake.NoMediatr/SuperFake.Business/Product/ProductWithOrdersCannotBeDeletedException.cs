namespace SuperFake.Business
{
    public class ProductWithOrdersCannotBeDeletedException : BusinessException
    {
        public ProductWithOrdersCannotBeDeletedException() : base("Product with orders cannot be deleted.")
        {
        }
    }
}