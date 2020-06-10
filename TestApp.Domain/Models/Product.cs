using System;
using System.Collections.Generic;

namespace TestApp.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }

        public DateTime PublishedDate { get; set; }
    }
}
