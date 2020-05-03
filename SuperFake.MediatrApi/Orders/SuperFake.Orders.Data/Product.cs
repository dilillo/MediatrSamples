using System.ComponentModel.DataAnnotations.Schema;

namespace SuperFake.Orders.Data
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
    }
}
