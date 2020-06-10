using AutoMapper;
using TestApp.WEB.Infrastructure.Interfaces;
using TestApp.WEB.Models.Orders;
using Microsoft.AspNetCore.Mvc;
using TestApp.Domain.Interfaces;
using TestApp.Domain.Models;

namespace TestApp.WEB.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargesController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IBasketManager _basketManager;

        public ChargesController(IOrderService orderService, IMapper mapper, IBasketManager basketManager)
        {
            _orderService = orderService;
            _mapper = mapper;
            _basketManager = basketManager;
        }

        [HttpPost]
        public IActionResult Charge([FromBody] ChargeViewModel model)
        {

            var basket = _basketManager.GetBasket();
            basket.Customer = _mapper.Map<Customer>(model.Order.Customer);
            var paymentResult = _orderService.ProcessPayment(basket, model.Token);

            if (paymentResult.IsSuccessful)
            {
                _basketManager.ClearBasket();

                return Ok(paymentResult);
            }

            return BadRequest(paymentResult);
        }
    }
}
