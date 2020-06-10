using TestApp.Domain.Models;

namespace TestApp.Domain.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(Order order);

        PaymentResult ProcessPayment(Models.Order order, string token);
    }
}
