namespace SuperFake.Domains
{
    public class DeleteCustomerWithOrdersCannotBeDeletedException : DomainException
    {
        public DeleteCustomerWithOrdersCannotBeDeletedException() : base("Customer with orders cannot be deleted.")
        {
        }
    }
}