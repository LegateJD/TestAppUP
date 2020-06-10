using TestApp.WEB.Models.Orders;
using System;
using System.ComponentModel.DataAnnotations;

namespace TestApp.WEB.Models.Customers
{
    public class CustomerViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        public OrderViewModel Order { get; set; }
    }
}
