using System.Collections.Generic;
using System.ComponentModel;

namespace SuperFake.Data
{
    public class Customer
    {
        public int ID { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Name")]
        public string FullName { get; set; }

        public List<Order> Orders { get; set; }
    }
}
