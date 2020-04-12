namespace SuperFake.Business
{
    public class CustomerNameMustBeUniqueException : BusinessException
    {
        public CustomerNameMustBeUniqueException() : base("Customer name must be unique.")
        {
        }
    }
}