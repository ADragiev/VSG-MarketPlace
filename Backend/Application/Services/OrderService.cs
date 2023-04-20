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

        public async Task CompleteOrder(int id)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, orderRepo);

            await orderRepo.SetField(id, "OrderStatus", OrderStatus.Finished);
        }

        public async Task<OrderGetDto> Create(OrderCreateDto dto)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(dto.ProductId, productRepo);
            var product = await productRepo.GetByID(dto.ProductId);

            ThrowExceptionService.ThrowExceptionWhenNotEnoughQuantity(product.SaleQty, dto.Qty);

            var newSaleQty = product.SaleQty - dto.Qty;
            await productRepo.SetField(product.Id, "SaleQty", newSaleQty);

            var newCombinedQty = product.CombinedQty - dto.Qty;
            await productRepo.SetField(product.Id, "CombinedQty", newCombinedQty);

            var order = mapper.Map<Order>(dto);
            order.ProductCode = product.Code;
            order.ProductName = product.FullName;
            order.Price = dto.Qty * product.Price;
            var orderId = await orderRepo.Create(order);

            var createdOrder = await orderRepo.GetByID(orderId);
            return mapper.Map<OrderGetDto>(createdOrder);
        }

        public async Task<List<OrderPendingDto>> GetAllPendingOrders()
        {
            var pendingOrders = await orderRepo.GetAllPendingOrders();
            pendingOrders.ForEach(o => o.OrderDate = FormatDate(o.OrderDate));
            return pendingOrders;
        }

        public async Task<List<OrderGetMineDto>> GetMyOrders(string email)
        {
            var myOrders= await orderRepo.GetMyOrders(email);
            myOrders.ForEach(o => o.OrderStatus = ((OrderStatus)int.Parse(o.OrderStatus)).ToString());
            myOrders.ForEach(o => o.OrderDate = FormatDate(o.OrderDate));
            return myOrders;
        }

        public async Task RejectOrder(int id)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, orderRepo);

            var order = await orderRepo.GetByID(id);
            ThrowExceptionService.ThrowExceptionWhenOrderIsNotPending(order);

            if (order.ProductId != null)
            {
                var product = await productRepo.GetByID((int)order.ProductId);

                var newSaleQty = product.SaleQty + order.Qty;
                await productRepo.SetField(product.Id, "SaleQty", newSaleQty);

                var newCombinedQty = product.CombinedQty + order.Qty;
                await productRepo.SetField(product.Id, "CombinedQty", newCombinedQty);
            }

            await orderRepo.Delete(id);
        }

        private string FormatDate(string dateString)
        {
            if(DateTime.TryParse(dateString, out var date))
            {
                return date.ToString("yyyy-MM-dd HH:mm");
            };
            throw new HttpException("Cannot parse date", HttpStatusCode.BadRequest);
        }
    }
}
