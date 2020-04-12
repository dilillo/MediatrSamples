namespace SuperFake.Domains
{
    public class DeleteProductWithOrdersCannotBeDeletedException : DomainException
    {
        public DeleteProductWithOrdersCannotBeDeletedException() : base("Product with orders cannot be deleted.")
        {
        }
    }
}