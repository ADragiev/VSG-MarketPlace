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

        [HttpGet("/Pending")]
        public List<OrderPendingDto> GetPendingOrders()
        {
            return orderService.GetAllPendingOrders();
        }

        [HttpGet("/MyOrders")]
        public List<OrderGetMineDto> GetMyOrders(string email)
        {
            return orderService.GetMyOrders(email);
        }


        [HttpPost]
        public OrderGetDto CreateOrder(OrderCreateDto dto)
        {
            return orderService.Create(dto);
        }
    }
}
