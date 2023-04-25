using Application.Models.ExceptionModels;
using Application.Models.GenericRepo;
using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
using Application.Models.ProductModels.Intefaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
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

        public OrderService(IOrderRepository orderRepo,
            IProductRepository productRepo,
            IMapper mapper)
        {
            this.orderRepo = orderRepo;
            this.productRepo = productRepo;
            this.mapper = mapper;
        }

        public async Task CompleteOrderAsync(int id)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, orderRepo);

            await orderRepo.SetFieldAsync(id, "Status", OrderStatus.Finished);
        }

        public async Task<OrderGetDto> CreateAsync(OrderCreateDto dto)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(dto.ProductId, productRepo);
            var product = await productRepo.GetByIdAsync(dto.ProductId);

            ThrowExceptionService.ThrowExceptionWhenNotEnoughQuantity(product.SaleQty, dto.Qty);

            var newSaleQty = product.SaleQty - dto.Qty;
            await productRepo.SetFieldAsync(product.Id, "SaleQty", newSaleQty);

            var newCombinedQty = product.CombinedQty - dto.Qty;
            await productRepo.SetFieldAsync(product.Id, "CombinedQty", newCombinedQty);

            var order = mapper.Map<Order>(dto);
            order.ProductCode = product.Code;
            order.ProductName = product.Name;
            order.Price = dto.Qty * product.Price;
            var orderId = await orderRepo.CreateAsync(order);

            var createdOrder = await orderRepo.GetByIdAsync(orderId);
            return mapper.Map<OrderGetDto>(createdOrder);
        }

        public async Task<List<OrderPendingDto>> GetAllPendingOrdersAsync()
        {
            var pendingOrders = await orderRepo.GetAllPendingOrdersAsync();
            pendingOrders.ForEach(o => o.Date = FormatDate(o.Date));
            return pendingOrders;
        }

        public async Task<List<OrderGetMineDto>> GetMyOrdersAsync(string email)
        {
            var myOrders= await orderRepo.GetMyOrdersAsync(email);
            myOrders.ForEach(o => o.Status = ((OrderStatus)int.Parse(o.Status)).ToString());
            myOrders.ForEach(o => o.Date = FormatDate(o.Date));
            return myOrders;
        }

        public async Task RejectOrderAsync(int id)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, orderRepo);

            var order = await orderRepo.GetByIdAsync(id);
            ThrowExceptionService.ThrowExceptionWhenOrderIsNotPending(order);

            if (order.ProductId != null)
            {
                var product = await productRepo.GetByIdAsync((int)order.ProductId);

                var newSaleQty = product.SaleQty + order.Qty;
                await productRepo.SetFieldAsync(product.Id, "SaleQty", newSaleQty);

                var newCombinedQty = product.CombinedQty + order.Qty;
                await productRepo.SetFieldAsync(product.Id, "CombinedQty", newCombinedQty);
            }

            await orderRepo.SetFieldAsync(id, "Status", OrderStatus.Declined);
        }

        private string FormatDate(string dateString)
        {
            if(DateTime.TryParse(dateString, CultureInfo.InvariantCulture,DateTimeStyles.None, out var date))
            {
                return date.ToString("yyyy-MM-dd HH:mm");
            };
            throw new HttpException("Cannot parse date", HttpStatusCode.BadRequest);
        }
    }
}
