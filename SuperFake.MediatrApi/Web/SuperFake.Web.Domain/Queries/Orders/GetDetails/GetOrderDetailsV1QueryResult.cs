using SuperFake.Shared.Data;
using System;
using System.Collections.Generic;

namespace SuperFake.Web.Domain
{
    public class GetOrderDetailsV1QueryResult
    {
        public int ID { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerID { get; set; }

        public string CustomerName { get; set; }

        public OrderStatuses OrderStatus { get; set; }

        public decimal TotalPrice { get; set; }

        public IEnumerable<GetOrderDetailsV1QueryResultOrderItem> OrderItems { get; set; }
    }

    public class GetOrderDetailsV1QueryResultOrderItem
    {
        public int ID { get; set; }

        public int Quantity { get; set; }

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
