using MediatR;
using SuperFake.Shared.Data;
using System;
using System.Collections.Generic;

namespace SuperFake.Shared.Domain
{
    public class OrderUpdatedV1Notification : INotification
    {
        public int ID { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerID { get; set; }

        public OrderStatuses OrderStatus { get; set; }

        public List<OrderUpdatedOrderItem> OrderItems { get; set; }

        public class OrderUpdatedOrderItem
        {
            public int ID { get; set; }

            public int Quantity { get; set; }

            public int ProductID { get; set; }

            public int OrderID { get; set; }
        }
    }
}
