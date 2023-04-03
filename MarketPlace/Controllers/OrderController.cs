using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("Pending")]
        public List<OrderPendingDto> GetPendingOrders()
        {
            return orderService.GetAllPendingOrders();
        }

        [HttpGet("MyOrders")]
        public List<OrderGetMineDto> GetMyOrders(string email)
        {
            return orderService.GetMyOrders(email);
        }


        [HttpPost("CompleteOrder/{id}")]
        public void CompleteOrder(int id)
        {
            orderService.CompleteOrder(id);
        }


        [HttpPost]
        public OrderGetDto CreateOrder(OrderCreateDto dto)
        {
            return orderService.Create(dto);
        }
    }
}
