using TestApp.WEB.Models.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestApp.Domain.Models;

namespace TestApp.WEB.Models.Orders
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public ShipmentMethod ShipmentMethod { get; set; }

        [Required]
        public CustomerViewModel Customer { get; set; }

        public List<OrderDetailsViewModel> OrderDetails { get; set; }
    }
}
