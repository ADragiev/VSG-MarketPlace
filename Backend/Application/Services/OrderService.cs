using Application.Helpers.Constants;
using Application.Models.ExceptionModels;
using Application.Models.GenericModels.Dtos;
using Application.Models.GenericRepo;
using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
using Application.Models.ProductModels.Intefaces;
using Application.Models.UserModels.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepo;
        private readonly IProductRepository productRepo;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public OrderService(IOrderRepository orderRepo,
            IProductRepository productRepo,
            IMapper mapper,
            IUserService userService)
        {
            this.orderRepo = orderRepo;
            this.productRepo = productRepo;
            this.mapper = mapper;
            this.userService = userService;
        }

        public async Task CompleteOrderAsync(int id)
        {
            var order = await orderRepo.GetByIdAsync(id);

            if(order == null)
            {
                throw new HttpException($"Order Id not found!", HttpStatusCode.NotFound);
            }

            if (order.Status != OrderStatus.Pending)
            {
                throw new HttpException("Only pending orders can be completed or declined!", HttpStatusCode.BadRequest);
            }

            await orderRepo.SetFieldAsync(id, "Status", OrderStatus.Finished);
        }

        public async Task CreateAsync(OrderCreateDto dto)
        {
            var product = await productRepo.GetByIdAsync(dto.ProductId);

            if (product == null || product.IsDeleted == true)
            {
                throw new HttpException($"Product Id not found!", HttpStatusCode.NotFound);
            }

            if (dto.Qty > product.SaleQty)
            {
                throw new HttpException("Not enough quantity for sale!", HttpStatusCode.BadRequest);
            }

            var newSaleQty = product.SaleQty - dto.Qty;
            await productRepo.SetFieldAsync(product.Id, "SaleQty", newSaleQty);

            var newCombinedQty = product.CombinedQty - dto.Qty;
            await productRepo.SetFieldAsync(product.Id, "CombinedQty", newCombinedQty);

            var order = mapper.Map<Order>(dto);
            order.ProductCode = product.Code;
            order.ProductName = product.Name;
            order.Price = dto.Qty * product.Price;
            order.OrderedBy = userService.GetUserEmail();
            await orderRepo.CreateAsync(order);
        }

        public async Task<List<OrderPendingDto>> GetAllPendingOrdersAsync()
        {
            var pendingOrders = await orderRepo.GetAllPendingOrdersAsync();
            return mapper.Map<List<OrderPendingDto>>(pendingOrders);
        }

        public async Task<List<OrderGetMineDto>> GetMyOrdersAsync()
        {
            var user = userService.GetUserEmail();
            var myOrders = await orderRepo.GetMyOrdersAsync(user);
            return mapper.Map<List<OrderGetMineDto>>(myOrders);
        }

        public async Task<GenericSimpleValueGetDto<string>> RejectOrderAsync(int id)
        {
            var order = await orderRepo.GetByIdAsync(id);

            if(order == null)
            {
                throw new HttpException($"Order Id not found!", HttpStatusCode.NotFound);
            }

            if (order.Status != OrderStatus.Pending)
            {
                throw new HttpException("Only pending orders can be completed or declined!", HttpStatusCode.BadRequest);
            }

            var user = userService.GetUserEmail();
            if (user != order.OrderedBy)
            {
                throw new HttpException("You cannot reject this order, because you are not its owner", HttpStatusCode.BadRequest);
            }

            var product = await productRepo.GetByIdAsync((int)order.ProductId);

            var newCombinedQty = product.CombinedQty + order.Qty;
            await productRepo.SetFieldAsync(product.Id, "CombinedQty", newCombinedQty);

            var newSaleQty = product.SaleQty + order.Qty;
            await productRepo.SetFieldAsync(product.Id, "SaleQty", newSaleQty);

            await orderRepo.SetFieldAsync(id, "Status", OrderStatus.Declined);

            return new GenericSimpleValueGetDto<string>(OrderStatus.Declined.ToString());
        }
    }
}
