using System.Collections.Generic;

namespace PillApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductCustomer> ProductCustomers { get; set; }
    }
}
