using Application.Helpers.Attributes;
using Application.Models.GenericModels.Dtos;
using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class OrderController : BaseController
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

        [HttpGet("My-Orders")]
        [NonAdmin]
        public async Task<List<OrderGetMineDto>> GetMyOrders()
        {
            return await orderService.GetMyOrdersAsync();
        }


        [HttpPut("Complete/{id}")]
        public async Task CompleteOrder(int id)
        {
            await orderService.CompleteOrderAsync(id);
        }

        [HttpPut("Reject/{id}")]
        [NonAdmin]
        public async Task<GenericSimpleValueGetDto<string>> RejectOrder(int id)
        {
            return await orderService.RejectOrderAsync(id);
        }


        [HttpPost]
        [NonAdmin]
        public async Task CreateOrder(OrderCreateDto dto)
        {
            await createValidator.ValidateAndThrowAsync(dto);
            await orderService.CreateAsync(dto);
        }
    }
}
