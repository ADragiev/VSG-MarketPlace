using Application.Models.GenericRepo;
using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepo;
        private readonly IGenericRepository<Product> productRepo;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository orderRepo,
            IGenericRepository<Product> productRepo,
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
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound<Product>(dto.ProductId, productRepo);

            var product = await productRepo.GetByID(dto.ProductId);

            ThrowExceptionService.ThrowExceptionWhenNotEnoughQuantity(product.SaleQty, dto.Qty);

            var newSaleQty = product.SaleQty - dto.Qty;
            await productRepo.SetField(product.Id, "SaleQty", newSaleQty);

            var newCombinedQty = product.CombinedQty - dto.Qty;
            await productRepo.SetField(product.Id, "CombinedQty", newCombinedQty);

            var order = mapper.Map<Order>(dto);
            var orderId = await orderRepo.Create(order);

            var createdOrder = await orderRepo.GetByID(orderId);
            return mapper.Map<OrderGetDto>(createdOrder);
        }

        public async Task<List<OrderPendingDto>> GetAllPendingOrders()
        {
            return await orderRepo.GetAllPendingOrders();
        }

        public async Task<List<OrderGetMineDto>> GetMyOrders(string email)
        {
            var myOrders= await orderRepo.GetMyOrders(email);
            myOrders.ForEach(o => o.OrderStatus = ((OrderStatus)int.Parse(o.OrderStatus)).ToString());
            return myOrders;
        }

        public async Task RejectOrder(int id)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, orderRepo);

            var order = await orderRepo.GetByID(id);
            ThrowExceptionService.ThrowExceptionWhenOrderIsNotPending(order);

            var product = await productRepo.GetByID(order.ProductId);

            var newSaleQty = product.SaleQty + order.Qty;
            await productRepo.SetField(product.Id, "SaleQty", newSaleQty);

            var newCombinedQty = product.CombinedQty + order.Qty;
            await productRepo.SetField(product.Id, "CombinedQty", newCombinedQty);

            await orderRepo.Delete(id);
        }
    }
}
