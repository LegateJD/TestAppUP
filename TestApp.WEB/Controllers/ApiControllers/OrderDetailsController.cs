using TestApp.WEB.Infrastructure.Interfaces;
using TestApp.WEB.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace TestApp.WEB.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IBasketManager _basketManager;

        public OrderDetailsController(IBasketManager basketManager)
        {
            _basketManager = basketManager;
        }

        [HttpDelete]
        public IActionResult RemoveOrderDetails([FromBody] DeleteProductViewModel model)
        {
            _basketManager.RemoveOrderDetails(model.ProductId);

            return Ok();
        }
    }
}
