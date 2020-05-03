using SuperFake.Shared.Data;
using System;
using System.Collections.Generic;

namespace SuperFake.Web.Domain
{
    public class GetCustomerDetailsV1QueryResult
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }


        public IEnumerable<GetCustomerDetailsV1QueryResultOrder> Orders { get; set; }
    }

    public class GetCustomerDetailsV1QueryResultOrder
    {
        public int ID { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatuses OrderStatus { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
