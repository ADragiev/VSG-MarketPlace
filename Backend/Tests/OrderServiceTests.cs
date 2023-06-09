﻿using Application.Helpers.Constants;
using Application.Models.ExceptionModels;
using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
using Application.Models.ProductModels.Intefaces;
using Application.Models.UserModels.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tests
{
    public class OrderServiceTests
    {
        private Mock<IOrderRepository> orderRepoMock;
        private Mock<IProductRepository> productRepoMock;
        private Mock<IMapper> mapperMock;
        private Mock<IUserService> userService;
        private IOrderService orderService;

        [SetUp]
        public void SetUp()
        {
            orderRepoMock = new Mock<IOrderRepository>();
            productRepoMock = new Mock<IProductRepository>();
            mapperMock = new Mock<IMapper>();
            userService = new Mock<IUserService>();
            orderService = new OrderService(orderRepoMock.Object, productRepoMock.Object, mapperMock.Object, userService.Object);
        }


        //CompleteOrder
        [Test]
        public void CompleteOrder_MustThrow_WhenOrderId_IsNotValid()
        {
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            Assert.ThrowsAsync<HttpException>(async () =>
            {
                await orderService.CompleteOrderAsync(1);
            });
        }

        [Test]
        public void CompleteOrder_MustNotThrow_WhenOrderId_IsValid()
        {
            int saleQty = 5;
            int combinedQty = 10;
            int orderQty = 5;
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateBaseOrder);

            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateBaseProduct());

            Assert.DoesNotThrowAsync(async () =>
            {
                await orderService.CompleteOrderAsync(1);
            });
        }

        [Test]
        public void CompleteOrder_MustThrow_WhenOrderStatus_IsNotPending()
        {
            var order = CreateBaseOrder();
            order.Status = OrderStatus.Declined;
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(order);

            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateBaseProduct());

            Assert.ThrowsAsync<HttpException>(async () =>
            {
                await orderService.CompleteOrderAsync(1);
            });
        }

        [Test]
        public void CompleteOrder_MustNotThrow_WhenOrderStatus_IsPending()
        {
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateBaseOrder());

            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateBaseProduct());

            Assert.DoesNotThrowAsync(async () =>
            {
                await orderService.CompleteOrderAsync(1);
            });
        }

        [Test]
        public async Task CompleteOrder_MustCallSetField_ForChanginOrderStatusToFinished()
        {
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateBaseOrder);

            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateBaseProduct());

            await orderService.CompleteOrderAsync(1);

            orderRepoMock.Verify(p => p.SetFieldAsync(1, "Status", OrderStatus.Finished), Times.Once);
        }
        //CompleteOrder


        //CreateAsync
        [Test]
        public void CreateOrder_MustNotThrow_WhenProductIdIsValid()
        {
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateBaseProduct());
            userService.Setup(u => u.GetUserEmail()).Returns("");
            mapperMock.Setup(m => m.Map<Order>(It.IsAny<OrderCreateDto>())).Returns(CreateBaseOrder());

            Assert.DoesNotThrowAsync(async () =>
            {
                await orderService.CreateAsync(new OrderCreateDto()
                {
                    ProductId = It.IsAny<int>(),
                    Qty = 2
                });
            });
        }

        [Test]
        public void CreateOrder_MustThrowWhen_ProductIdIsNotValid()
        {
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            Assert.ThrowsAsync<HttpException>(async () =>
            {
                await orderService.CreateAsync(new OrderCreateDto()
                {
                    ProductId = It.IsAny<int>(),
                    Qty = 2
                });
            });
        }


        [Test]
        public void CreateOrder_MustNotThrow_WhenSaleQtyIsMoreThanOrEqualToOrderQty()
        {
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateBaseProduct());
            userService.Setup(u=> u.GetUserEmail()).Returns("User");
            mapperMock.Setup(m => m.Map<Order>(It.IsAny<OrderCreateDto>())).Returns(CreateBaseOrder());

            Assert.DoesNotThrowAsync(async () =>
            {
                await orderService.CreateAsync(new OrderCreateDto()
                {
                    ProductId = 1,
                    Qty = 2
                });
            });
        }

        [Test]
        public void CreateOrder_MustThrow_WhenOrderQtyIsMoreThanSaleQty()
        {
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateBaseProduct());

            var order = CreateBaseOrder();
            order.Qty = 500;
            mapperMock.Setup(m => m.Map<Order>(It.IsAny<OrderCreateDto>())).Returns(order);

            Assert.ThrowsAsync<HttpException>(async () =>
            {
                await orderService.CreateAsync(new OrderCreateDto()
                {
                    ProductId = 1,
                    Qty = 500
                });
            });
        }

        [Test]
        public async Task CreateOrder_MustChange_ProductCombinedAndSaleQty()
        {
            var product = CreateBaseProduct();
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
            userService.Setup(u => u.GetUserEmail()).Returns("User");
            var order = CreateBaseOrder();
            mapperMock.Setup(m => m.Map<Order>(It.IsAny<OrderCreateDto>())).Returns(order);

            await orderService.CreateAsync(new OrderCreateDto()
            {
                ProductId = It.IsAny<int>(),
                Qty = 2
            });

            productRepoMock.Verify(p => p.SetFieldAsync(1, "SaleQty", product.SaleQty - order.Qty), Times.Once);
            productRepoMock.Verify(p => p.SetFieldAsync(1, "CombinedQty", product.CombinedQty - order.Qty), Times.Once);
        }
        //CreateAsync


        //RejectOrder
        [Test]
        public void RejectOrder_MustThrow_WhenOrderId_IsNotValid()
        {
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            Assert.ThrowsAsync<HttpException>(async () =>
            {
                await orderService.RejectOrderAsync(1);
            });
        }

        [Test]
        public void RejectOrder_MustNotThrow_WhenOrderId_IsValid()
        {
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateBaseOrder());
            userService.Setup(u => u.GetUserEmail()).Returns("User");
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateBaseProduct());

            Assert.DoesNotThrowAsync(async () =>
            {
                await orderService.RejectOrderAsync(213);
            });
        }

        [Test]
        public void RejectOrder_MustNotThrow_WhenOrderStatus_IsPending()
        {
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateBaseOrder());
            userService.Setup(u => u.GetUserEmail()).Returns("User");
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateBaseProduct());

            Assert.DoesNotThrowAsync(async () =>
            {
                await orderService.RejectOrderAsync(1);
            });
        }

        [Test]
        public void RejectOrder_MustThrow_WhenOrderStatus_IsNotPending()
        {
            var order = CreateBaseOrder();
            order.Status = OrderStatus.Finished;
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(order);

            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateBaseProduct());

            Assert.ThrowsAsync<HttpException>(async () =>
            {
                await orderService.RejectOrderAsync(1);
            });
        }

        [Test]
        public async Task RejectOrder_MustCallSetField_ForReturnignProductQty()
        {
            var order = CreateBaseOrder();
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(order);
            userService.Setup(u => u.GetUserEmail()).Returns("User");
            var product = CreateBaseProduct();
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            await orderService.RejectOrderAsync(1);

            productRepoMock.Verify(p => p.SetFieldAsync(1, "SaleQty", order.Qty + product.SaleQty), Times.Once);
            productRepoMock.Verify(p => p.SetFieldAsync(1, "CombinedQty", order.Qty + product.CombinedQty), Times.Once);
        }


        [Test]
        public async Task RejectOrder_MustCallSetField_ForChanginOrderStatusToRejected()
        {
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateBaseOrder);
            userService.Setup(u => u.GetUserEmail()).Returns("User");
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateBaseProduct());

            await orderService.RejectOrderAsync(1);

            orderRepoMock.Verify(p => p.SetFieldAsync(1, "Status", OrderStatus.Declined), Times.Once);
        }
        //RejectOrder


        private static Product CreateBaseProduct()
        {
            return new Product()
            {
                Id = 1,
                Code = "Code",
                Name = "Name",
                Description = "Descr",
                Price = 200,
                SaleQty = 5,
                CombinedQty = 10,
                CategoryId = 1,
                LocationId = 1
            };
        }

        private static Order CreateBaseOrder()
        {
            return new Order()
            {
                Id = 1,
                ProductCode = "Code",
                ProductName = "Name",
                Price = 400,
                Qty = 2,
                OrderedBy = "User",
                Date = DateTime.Now,
                Status = OrderStatus.Pending,
                ProductId = 1
            };
        }

        private static OrderGetMineDto CreateBaseMyOrder()
        {
            return new OrderGetMineDto()
            {
                Id = 1,
                ProductName = "Name",
                Price = 400,
                Qty = 2,
                Date = DateTime.Now.ToString(),
                Status = ((int)OrderStatus.Pending).ToString()
            };
        }

        private static OrderPendingDto CreateBasePendingOrder()
        {
            return new OrderPendingDto()
            {
                Id = 1,
                ProductCode = "Code",
                Qty = 2,
                Price = 400,
                Date = DateTime.Now.ToString(),
                OrderedBy = "User"
            };
        }
    }
}
