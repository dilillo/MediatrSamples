using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SuperFake.Data
{
    public class Order
    {
        public int ID { get; set; }

        [DisplayName("Order Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime OrderDate { get; set; }

        [DisplayName("Customer")]
        public int CustomerID { get; set; }

        [DisplayName("Status")]
        public OrderStatuses OrderStatus { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public Customer Customer { get; set; }
    }
}
