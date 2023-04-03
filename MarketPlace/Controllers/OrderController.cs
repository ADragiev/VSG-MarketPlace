using Application.Models.OrderModels.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public void Test()
        {
            orderService.Test();
        }
    }
}
