using SuperFake.Shared.Data;
using System;
using System.Collections.Generic;

namespace SuperFake.Orders.Data
{
    public class Order
    {
        public int ID { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerID { get; set; }

        public OrderStatuses OrderStatus { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
