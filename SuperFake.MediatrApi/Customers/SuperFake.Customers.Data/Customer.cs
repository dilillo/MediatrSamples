using System.ComponentModel;

namespace SuperFake.Customers.Data
{
    public class Customer
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool HasOrders { get; set; }
    }
}
