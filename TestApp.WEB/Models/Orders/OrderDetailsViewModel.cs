using TestApp.WEB.Models.Product;

namespace TestApp.WEB.Models.Orders
{
    public class OrderDetailsViewModel
    {
        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public ShowProductViewModel Product { get; set; }
    }
}
