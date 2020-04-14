using System.ComponentModel.DataAnnotations;

namespace SuperFake.Orders.Data
{
    public class OrderItem
    {
        public int ID { get; set; }

        [Range(1, 1000)]
        public int Quantity { get; set; }

        public int ProductID { get; set; }

        public int OrderID { get; set; }
    }
}
