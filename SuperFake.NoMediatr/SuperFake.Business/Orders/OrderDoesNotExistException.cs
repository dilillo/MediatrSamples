namespace SuperFake.Business
{
    public class OrderDoesNotExistException : BusinessException
    {
        public OrderDoesNotExistException() : base("Order does not exist.")
        {
        }
    }
}