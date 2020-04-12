namespace SuperFake.Business
{
    public class CustomerDoesNotExistException : BusinessException
    {
        public CustomerDoesNotExistException() : base("Customer does not exist.")
        {
        }
    }
}