using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperFake.Web.Data
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
