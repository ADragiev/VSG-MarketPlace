using Application.Models.OrderModels.Dtos;
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

        [HttpPost]
        public OrderGetDto CreateOrder(OrderCreateDto dto)
        {
            return orderService.Create(dto);
        }
    }
}
