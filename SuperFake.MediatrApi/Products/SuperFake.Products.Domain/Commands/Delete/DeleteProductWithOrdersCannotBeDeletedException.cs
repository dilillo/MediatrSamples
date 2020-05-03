using SuperFake.Shared.Domain;

namespace SuperFake.Products.Domain
{
    public class DeleteProductWithOrdersCannotBeDeletedException : DomainException
    {
        public DeleteProductWithOrdersCannotBeDeletedException() : base("Product with orders cannot be deleted.")
        {
        }
    }
}