namespace SuperFake.Business
{
    public class CustomerWithOrdersCannotBeDeletedException : BusinessException
    {
        public CustomerWithOrdersCannotBeDeletedException() : base("Customer with orders cannot be deleted.")
        {
        }
    }
}