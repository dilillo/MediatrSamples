namespace SuperFake.Domains
{
    public class UpdateCustomerNameMustBeUniqueException : DomainException
    {
        public UpdateCustomerNameMustBeUniqueException() : base("Customer name must be unique.")
        {
        }
    }
}