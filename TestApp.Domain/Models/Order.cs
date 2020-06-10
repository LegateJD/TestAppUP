using System;
using System.Collections.Generic;

namespace TestApp.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public ShipmentMethod ShipmentMethod { get; set; }

        public Customer Customer { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }
    }
}
