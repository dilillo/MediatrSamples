namespace SuperFake.Domains
{
    public class UpdateProductDoesNotExistException : DomainException
    {
        public UpdateProductDoesNotExistException() : base("Product does not exist.")
        {
        }
    }
}
