using SuperFake.Shared.Data;
using System;

namespace SuperFake.Web.Domain
{
    public class GetAllOrdersV1QueryResult
    {
        public int ID { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerID { get; set; }

        public string CustomerName { get; set; }

        public OrderStatuses OrderStatus { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
