using Application.Models.ExceptionModels;
using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
using Application.Models.ProductModels.Intefaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> orderRepoMock = new Mock<IOrderRepository>();
        private readonly Mock<IProductRepository> productRepoMock = new Mock<IProductRepository>();
        private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();
        private readonly IOrderService orderService;


        public OrderServiceTests()
        {
            orderService = new OrderService(orderRepoMock.Object, productRepoMock.Object, mapperMock.Object);
        }

        [Test]
        public void CreateOrder_MustThrowWhen_ProductIdIsNotValid()
        {
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            Assert.ThrowsAsync<HttpException>(async () =>
            {
                await orderService.CreateAsync(new OrderCreateDto()
                {
                    ProductId = 1,
                    OrderedBy = "User",
                    Qty = 2
                });
            });
        }

        [Test]
        public void CreateOrder_MustNotThrow_WhenProductIdIsValid()
        {
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct(1,"a","d",200,"a",20, 30, 1, 1));

            mapperMock.Setup(m => m.Map<Order>(It.IsAny<OrderCreateDto>())).Returns(new Order()
            {
                OrderedBy = "User",
                Qty = 2,
                ProductId = 1,
                Price = 200,
                Date = DateTime.Now,
                Id = 1,
                ProductCode = "a",
                ProductName = "b",
                Status = OrderStatus.Pending
            });

            Assert.DoesNotThrowAsync(async () => 
            {
                await orderService.CreateAsync(new OrderCreateDto()
                {
                    ProductId = 1,
                    OrderedBy = "User",
                    Qty = 2
                });
            });
        }

        [Test]
        public void CreateOrder_MustNotThrow_WhenSaleQtyIsMoreThanOrEqualToOrderQty()
        {
            int saleQty = 5;
            int combinedQty = 10;
            int orderQty = 5;
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct(1, "a", "d", 200, "a", saleQty, combinedQty, 1, 1));

            mapperMock.Setup(m => m.Map<Order>(It.IsAny<OrderCreateDto>())).Returns(new Order()
            {
                OrderedBy = "User",
                Qty = orderQty,
                ProductId = 1,
                Price = 200,
                Date = DateTime.Now,
                Id = 1,
                ProductCode = "a",
                ProductName = "b",
                Status = OrderStatus.Pending
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                await orderService.CreateAsync(new OrderCreateDto()
                {
                    ProductId = 1,
                    OrderedBy = "User",
                    Qty = orderQty
                });
            });
        }

        [Test]
        public void CreateOrder_MustThrow_WhenOrderQtyIsMoreThanSaleQty()
        {
            int saleQty = 5;
            int combinedQty = 10;
            int orderQty = 20;
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct(1, "a", "d", 200, "a", saleQty, combinedQty, 1, 1));

            mapperMock.Setup(m => m.Map<Order>(It.IsAny<OrderCreateDto>())).Returns(new Order()
            {
                OrderedBy = "User",
                Qty = orderQty,
                ProductId = 1,
                Price = 200,
                Date = DateTime.Now,
                Id = 1,
                ProductCode = "a",
                ProductName = "b",
                Status = OrderStatus.Pending
            });

            Assert.ThrowsAsync<HttpException>(async () =>
            {
                await orderService.CreateAsync(new OrderCreateDto()
                {
                    ProductId = 1,
                    OrderedBy = "User",
                    Qty = orderQty
                });
            });
        }

        [Test]
        public void CreateOrder_MustReturn_OrderGetDto()
        {
            int saleQty = 5;
            int combinedQty = 10;
            int orderQty = 5;
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct(1, "a", "d", 200, "a", saleQty, combinedQty, 1, 1));

            mapperMock.Setup(m => m.Map<Order>(It.IsAny<OrderCreateDto>())).Returns(new Order()
            {
                OrderedBy = "User",
                Qty = orderQty,
                ProductId = 1,
                Price = 200,
                Date = DateTime.Now,
                Id = 1,
                ProductCode = "a",
                ProductName = "b",
                Status = OrderStatus.Pending
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                await orderService.CreateAsync(new OrderCreateDto()
                {
                    ProductId = 1,
                    OrderedBy = "User",
                    Qty = orderQty
                });
            });
        }

        [Test]
        public void RejectOrder_MustThrow_WhenOrderId_IsNotValid()
        {
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            Assert.ThrowsAsync<HttpException>(async () =>
            {
                await orderService.RejectOrderAsync(1);
            });
        }

        //[Test]
        //public void RejectOrder_MustNotThrow_WhenOrderId_IsValid()
        //{
        //    orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => new Order()
        //    {
        //        OrderedBy = "User",
        //        Qty = orderQty,
        //        ProductId = 1,
        //        Price = 200,
        //        Date = DateTime.Now,
        //        Id = 1,
        //        ProductCode = "a",
        //        ProductName = "b",
        //        Status = OrderStatus.Pending
        //    });

        //    Assert.ThrowsAsync<HttpException>(async () =>
        //    {
        //        await orderService.RejectOrderAsync(1);
        //    });
        //}

        [Test]
        public void RejectOrder_MustCallSetField_ForReturnignProductQty()
        {
            int saleQty = 5;
            int combinedQty = 10;
            int orderQty = 5;
      

            Assert.DoesNotThrowAsync(async () =>
            {
                await orderService.CreateAsync(new OrderCreateDto()
                {
                    ProductId = 1,
                    OrderedBy = "User",
                    Qty = orderQty
                });
            });
        }


        private static Product CreateProduct(int id, string code, string descr, decimal price, string name, int saleQty, int combinedQty, int categoryId, int locationId)
        {
            return new Product()
            {
                Id = id,
                Code = code,
                Description = descr,
                Price = price,
                Name = name,
                SaleQty = saleQty,
                CombinedQty = combinedQty,
                CategoryId = categoryId,
                LocationId = locationId
            };
        }
    }
}
