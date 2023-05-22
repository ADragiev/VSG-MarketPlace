using Application.Helpers.Constants;
using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = IdentityConstants.AdminRolePolicyName)]
        public async Task<List<OrderPendingDto>> GetPendingOrders()
        {
            return await orderService.GetAllPendingOrdersAsync();
        }

        [HttpGet("/MyOrders")]
        public async Task<List<OrderGetMineDto>> GetMyOrders()
        {
            return await orderService.GetMyOrdersAsync();
        }


        [HttpPut("{id}")]
        [Authorize(Policy = IdentityConstants.AdminRolePolicyName)]
        public async Task CompleteOrder(int id)
        {
            await orderService.CompleteOrderAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task<OrderStatusGetDto> RejectOrder(int id)
        {
            return await orderService.RejectOrderAsync(id);
        }


        [HttpPost]
        public async Task CreateOrder(OrderCreateDto dto)
        {
            await createValidator.ValidateAndThrowAsync(dto);
            await orderService.CreateAsync(dto);
        }
    }
}
