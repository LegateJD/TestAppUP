using AutoMapper;
using TestApp.WEB.Infrastructure.Interfaces;
using TestApp.WEB.Models.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using TestApp.Domain.Interfaces;
using TestApp.Domain.Models;

namespace TestApp.WEB.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IBasketManager _basketManager;

        public OrdersController(IOrderService orderService, IMapper mapper, IBasketManager basketManager)
        {
            _orderService = orderService;
            _mapper = mapper;
            _basketManager = basketManager;
        }

        [Route("basket", Name = "defaultGetBasket")]
        [HttpGet]
        public IActionResult GetBasket()
        {
            var viewModel = _mapper.Map<OrderViewModel>(_basketManager.GetBasket());

            return View(viewModel);
        }

        [Route("order", Name = "defaultMakeOrder")]
        [HttpGet]
        public IActionResult MakeOrder()
        {
            var viewModel = _mapper.Map<OrderViewModel>(_basketManager.GetBasket());

            if(viewModel.OrderDetails == null || !viewModel.OrderDetails.Any())
            {
                return RedirectToRoute("defaultGetBasket");
            }

            return View(viewModel);
        }

        [Route("payment", Name = "defaultDoPayment")]
        [HttpPost]
        public IActionResult DoPayment(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = _mapper.Map<OrderViewModel>(_basketManager.GetBasket());

                return View("MakeOrder", viewModel);
            }

            var basket = _basketManager.GetBasket();

            if (model.PaymentMethod == PaymentMethod.Card)
            {
                model.OrderDetails = _mapper.Map<List<OrderDetailsViewModel>>(basket.OrderDetails);

                return View("CardPayment", model);
            }

            var order = _mapper.Map<Order>(model);
            order.OrderDetails = basket.OrderDetails;
            _orderService.CreateOrder(order);
            _basketManager.ClearBasket();

            return View("OrderPlacing");
        }

        [Route("paymentResult", Name = "defaultPaymentResult")]
        [HttpPost]
        public IActionResult PaymentResult(PaymentResultViewModel model)
        {
            if (model.IsSuccessful)
            {
                return View("SuccessfulPayment");

            }

            return View("FailedPayment");
        }

        [Route("product/buy", Name = "defaultAddProductToOrderPost")]
        [HttpPost]
        public IActionResult AddProductToOrder(CreateOrderDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToRoute("defaultGetProduct", new { productId = model.ProductId });
            }

            _basketManager.AddOrderDetails(model.ProductId, model.Quantity);

            return RedirectToRoute("defaultGetBasket");
        }

        [Route("basket/table")]
        public IActionResult BasketTable()
        {
            var viewModel = _mapper.Map<OrderViewModel>(_basketManager.GetBasket());

            return PartialView("_Basket", viewModel);
        }
    }
}
