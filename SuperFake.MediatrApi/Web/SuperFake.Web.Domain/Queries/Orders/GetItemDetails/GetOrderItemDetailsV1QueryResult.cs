namespace SuperFake.Web.Domain
{
    public class GetOrderItemDetailsV1QueryResult
    {
        public int ID { get; set; }

        public int Quantity { get; set; }

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public int OrderID { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
