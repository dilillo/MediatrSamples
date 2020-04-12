using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SuperFake.Data
{
    public class OrderItem
    {
        public int ID { get; set; }

        [Range(1, 1000)]
        public int Quantity { get; set; }

        [DisplayName("Product")]
        public int ProductID { get; set; }

        public int OrderID { get; set; }

        public Product Product { get; set; }

        public Order Order { get; set; }
    }
}
