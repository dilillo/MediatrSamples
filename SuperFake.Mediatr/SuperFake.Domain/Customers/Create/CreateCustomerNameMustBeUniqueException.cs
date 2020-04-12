namespace SuperFake.Domains
{
    public class CreateCustomerNameMustBeUniqueException : DomainException
    {
        public CreateCustomerNameMustBeUniqueException() : base("Customer name must be unique.")
        {
        }
    }
}