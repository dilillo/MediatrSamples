namespace SuperFake.Domains
{
    public class DeleteProductDoesNotExistException : DomainException
    {
        public DeleteProductDoesNotExistException() : base("Product does not exist.")
        {
        }
    }
}
