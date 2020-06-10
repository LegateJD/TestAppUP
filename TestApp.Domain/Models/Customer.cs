using System;

namespace TestApp.Domain.Models
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }
    }
}
