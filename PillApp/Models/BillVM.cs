using System;

namespace PillApp.Models
{
    public class BillVM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Value { get; set; }
        public DateTime DateBill { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }
    }
}
