namespace SuperFake.Business
{
    public class OrderIsShippedAndCannotBeChangedException : BusinessException
    {
        public OrderIsShippedAndCannotBeChangedException(): base("Order is shipped and cannot be changed.")
        {
        }
    }
}