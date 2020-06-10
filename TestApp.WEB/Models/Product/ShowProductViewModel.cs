using System;

namespace TestApp.WEB.Models.Product
{
    public class ShowProductViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
