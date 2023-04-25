using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IValidator<OrderCreateDto> createValidator;

        public OrderController(IOrderService orderService,
            IValidator<OrderCreateDto> createValidator)
        {
            this.orderService = orderService;
            this.createValidator = createValidator;
        }

        [HttpGet]
        public async Task<List<OrderPendingDto>> GetPendingOrders()
        {
            return await orderService.GetAllPendingOrdersAsync();
        }

        [HttpGet("{username}")]
        public async Task<List<OrderGetMineDto>> GetMyOrders(string username)
        {
            return await orderService.GetMyOrdersAsync(username);
        }


        [HttpPut("{id}")]
        public async Task CompleteOrder(int id)
        {
            await orderService.CompleteOrderAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task RejectOrder(int id)
        {
            await orderService.RejectOrderAsync(id);
        }


        [HttpPost]
        public async Task<OrderGetDto> CreateOrder(OrderCreateDto dto)
        {
            await createValidator.ValidateAndThrowAsync(dto);
            return await orderService.CreateAsync(dto);
        }
    }
}
