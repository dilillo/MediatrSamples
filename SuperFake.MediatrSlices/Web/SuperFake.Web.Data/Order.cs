using SuperFake.Shared.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperFake.Web.Data
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [DisplayName("Order Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime OrderDate { get; set; }

        [DisplayName("Customer")]
        public int CustomerID { get; set; }

        [DisplayName("Status")]
        public OrderStatuses OrderStatus { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalPrice { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public Customer Customer { get; set; }
    }
}
