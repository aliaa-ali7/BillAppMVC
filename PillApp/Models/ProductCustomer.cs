﻿namespace PillApp.Models
{
    public class ProductCustomer
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
