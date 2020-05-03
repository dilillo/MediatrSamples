using System;
using System.Collections.Generic;
using System.Text;

namespace SuperFake.Web.Domain
{
    public class GetAllProductsV1QueryResult
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
