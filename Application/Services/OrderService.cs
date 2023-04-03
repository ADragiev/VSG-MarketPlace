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
        private readonly IGenericRepository<Order> orderRepo;
        private readonly IGenericRepository<Product> productRepo;
        private readonly IMapper mapper;

        public OrderService(IGenericRepository<Order> orderRepo,
            IGenericRepository<Product> productRepo,
            IMapper mapper)
        {
            this.orderRepo = orderRepo;
            this.productRepo = productRepo;
            this.mapper = mapper;
        }

        public OrderGetDto Create(OrderCreateDto dto)
        {
            ThrowExceptionService.ThrowExceptionWhenIdNotFound<Product>(dto.ProductCode, productRepo);

            var product = productRepo.GetByID(dto.ProductCode);

            ThrowExceptionService.ThrowExceptionWhenNotEnoughQuantity(product.SaleQty, dto.Qty);

            var newSaleQty = product.SaleQty - dto.Qty;
            productRepo.SetField(product.Id, "SaleQty", newSaleQty);

            var newCombinedQty = product.CombinedQty - dto.Qty;
            productRepo.SetField(product.Id, "CombinedQty", newCombinedQty);

            var order = mapper.Map<Order>(dto);
            var orderId = orderRepo.Create(order);

            var createdOrder = orderRepo.GetByID(orderId);
            return mapper.Map<OrderGetDto>(createdOrder);
        }
    }
}
