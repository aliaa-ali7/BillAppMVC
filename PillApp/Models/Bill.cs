using System;
using System.Collections.Generic;

namespace PillApp.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime DateBill { get; set; }
        public List<Product> Products { get; set; }
    }
}
