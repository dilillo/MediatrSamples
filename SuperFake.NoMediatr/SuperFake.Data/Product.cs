using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperFake.Data
{
    public class Product
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
