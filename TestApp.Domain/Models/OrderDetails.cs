using System;

namespace TestApp.Domain.Models
{
    public class OrderDetails
    {
        public Guid Id { get; set; }

        public short Quantity { get; set; }

        public decimal Price { get; set; }

        public Product Product { get; set; }

        public Order Order { get; set; }
    }
}
