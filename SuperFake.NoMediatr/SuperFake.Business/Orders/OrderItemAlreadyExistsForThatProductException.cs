namespace SuperFake.Business
{
    public class OrderItemAlreadyExistsForThatProductException : BusinessException
    {
        public OrderItemAlreadyExistsForThatProductException() : base("Order item already exists for that product.")
        {
        }
    }
}