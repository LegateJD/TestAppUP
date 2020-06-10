using System;
using System.ComponentModel.DataAnnotations;

namespace TestApp.WEB.Models.Orders
{
    public class CreateOrderDetailsViewModel
    {
        public Guid ProductId { get; set; }

        [Range(1, 200)]
        public short Quantity { get; set; }
    }
}
