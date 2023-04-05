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
        public async Task<List<OrderPendingDto>> GetPendingOrders()
        {
            return await orderService.GetAllPendingOrders();
        }

        [HttpGet("MyOrders")]
        public async Task<List<OrderGetMineDto>> GetMyOrders(string email)
        {
            return await orderService.GetMyOrders(email);
        }


        [HttpPut("CompleteOrder/{id}")]
        public async Task CompleteOrder(int id)
        {
            await orderService.CompleteOrder(id);
        }

        [HttpDelete("Reject/{id}")]
        public async Task RejectOrder(int id)
        {
            await orderService.RejectOrder(id);
        }


        [HttpPost]
        public async Task<OrderGetDto> CreateOrder(OrderCreateDto dto)
        {
            return await orderService.Create(dto);
        }
    }
}
