using TestApp.Domain.Interfaces.Repositories;
using TestApp.Domain.Interfaces.UnitsOfWork;
using TestApp.Domain.Interfaces;
using TestApp.Domain.Models;
using Microsoft.Extensions.Logging;
using Stripe;
using System.Linq;

namespace TestApp.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Models.Order> _orderRepository;

        public OrderService(IUnitOfWork unitOfWork, IRepository<Models.Order> orderRepository, ILogger<OrderService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public PaymentResult ProcessPayment(Models.Order order, string token)
        {
            var price = order.OrderDetails.Select(c => c.Quantity * c.Product.Price).Sum();
            var options = new ChargeCreateOptions
            {
                Amount = (long)(price * 100),
                Currency = "usd",
                Description = "Charge",
                Source = token,
            };

            var service = new ChargeService();
            Charge charge = null;
            var paymentResult = new PaymentResult();

            try
            {
                charge = service.Create(options);
                paymentResult.IsSuccessful = charge.Status == "succeeded";
            }
            catch(StripeException exception)
            {
                paymentResult.Error = exception.StripeError.Message;
                paymentResult.IsSuccessful = false;
                _logger.LogInformation($"Charge has failed|{exception.StripeError.Message}");
            }

            if (paymentResult.IsSuccessful)
            {
                order.PaymentMethod = Models.PaymentMethod.Card;
                CreateOrder(order);
            }

            return paymentResult;
        }

        public void CreateOrder(Models.Order order)
        {
            foreach(var item in order.OrderDetails)
            {
                item.Price = item.Product.Price;
            }

            _orderRepository.Insert(order);
            _unitOfWork.Commit();
        }

    }
}
