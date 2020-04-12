namespace SuperFake.Business
{
    public class OrderItemDoesNotExistException : BusinessException
    {
        public OrderItemDoesNotExistException() : base("Order item does not exist.")
        {
        }
    }
}