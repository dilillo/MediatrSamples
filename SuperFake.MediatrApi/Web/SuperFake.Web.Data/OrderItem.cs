using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperFake.Web.Data
{
    public class OrderItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Range(1, 1000)]
        public int Quantity { get; set; }

        [DisplayName("Product")]
        public int ProductID { get; set; }

        public int OrderID { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalPrice { get; set; }

        public Product Product { get; set; }

        public Order Order { get; set; }
    }
}
