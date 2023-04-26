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
        private Mock<IOrderRepository> orderRepoMock;
        private Mock<IProductRepository> productRepoMock;
        private Mock<IMapper> mapperMock = new Mock<IMapper>();
        private  IOrderService orderService;

        [SetUp]
        public void SetUp()
        {
            orderRepoMock = new Mock<IOrderRepository>();
            productRepoMock = new Mock<IProductRepository>();
            mapperMock = new Mock<IMapper>();
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
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct(1, "a", "d", 200, "a", 20, 30, 1, 1));

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

        [Test]
        public void RejectOrder_MustNotThrow_WhenOrderId_IsValid()
        {
            int saleQty = 5;
            int combinedQty = 10;
            int orderQty = 5;
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => new Order()
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

            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct(1, "a", "d", 200, "a", saleQty, combinedQty, 1, 1));

            Assert.DoesNotThrowAsync(async () =>
            {
                await orderService.RejectOrderAsync(1);
            });
        }


        [Test]
        public void RejectOrder_MustNotThrow_WhenOrderStatus_IsPending()
        {
            int saleQty = 5;
            int combinedQty = 10;
            int orderQty = 5;
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => new Order()
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

            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct(1, "a", "d", 200, "a", saleQty, combinedQty, 1, 1));

            Assert.DoesNotThrowAsync(async () =>
            {
                await orderService.RejectOrderAsync(1);
            });
        }

        [Test]
        public void RejectOrder_MustThrow_WhenOrderStatus_IsNotPending()
        {
            int saleQty = 5;
            int combinedQty = 10;
            int orderQty = 5;
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => new Order()
            {
                OrderedBy = "User",
                Qty = orderQty,
                ProductId = 1,
                Price = 200,
                Date = DateTime.Now,
                Id = 1,
                ProductCode = "a",
                ProductName = "b",
                Status = OrderStatus.Finished
            });

            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct(1, "a", "d", 200, "a", saleQty, combinedQty, 1, 1));

            Assert.ThrowsAsync<HttpException>(async () =>
            {
                await orderService.RejectOrderAsync(1);
            });
        }

        [Test]
        public async Task RejectOrder_MustCallSetField_ForReturnignProductQty()
        {
            int saleQty = 5;
            int combinedQty = 10;
            int orderQty = 5;
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => new Order()
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

            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct(1, "a", "d", 200, "a", saleQty, combinedQty, 1, 1));

            await orderService.RejectOrderAsync(1);

            productRepoMock.Verify(p => p.SetFieldAsync(1, "SaleQty", orderQty + saleQty), Times.Once);
            productRepoMock.Verify(p => p.SetFieldAsync(1, "CombinedQty", orderQty + combinedQty), Times.Once);
        }

        [Test]
        public async Task RejectOrder_MustNotCallSetField_ForReturnignProductQty_IfProductIdIsNull()
        {
            int saleQty = 5;
            int combinedQty = 10;
            int orderQty = 5;
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => new Order()
            {
                OrderedBy = "User",
                Qty = orderQty,
                ProductId = null,
                Price = 200,
                Date = DateTime.Now,
                Id = 1,
                ProductCode = "a",
                ProductName = "b",
                Status = OrderStatus.Pending
            });

            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct(1, "a", "d", 200, "a", saleQty, combinedQty, 1, 1));

            await orderService.RejectOrderAsync(1);

            productRepoMock.Verify(p => p.SetFieldAsync(1, "SaleQty", It.IsAny<int>()), Times.Never);
            productRepoMock.Verify(p => p.SetFieldAsync(1, "CombinedQty", It.IsAny<int>()), Times.Never);
        }

        [Test]
        public async Task RejectOrder_MustCallSetField_ForChanginOrderStatusToRejected()
        {
            int saleQty = 5;
            int combinedQty = 10;
            int orderQty = 5;
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => new Order()
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

            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct(1, "a", "d", 200, "a", saleQty, combinedQty, 1, 1));

            await orderService.RejectOrderAsync(1);

            orderRepoMock.Verify(p => p.SetFieldAsync(1, "Status", OrderStatus.Declined), Times.Once);
        }


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
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => new Order()
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

            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct(1, "a", "d", 200, "a", saleQty, combinedQty, 1, 1));

            Assert.DoesNotThrowAsync(async () =>
            {
                await orderService.CompleteOrderAsync(1);
            });
        }

        [Test]
        public void CompleteOrder_MustThrow_WhenOrderStatus_IsNotPending()
        {
            int saleQty = 5;
            int combinedQty = 10;
            int orderQty = 5;
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => new Order()
            {
                OrderedBy = "User",
                Qty = orderQty,
                ProductId = 1,
                Price = 200,
                Date = DateTime.Now,
                Id = 1,
                ProductCode = "a",
                ProductName = "b",
                Status = OrderStatus.Finished
            });

            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct(1, "a", "d", 200, "a", saleQty, combinedQty, 1, 1));

            Assert.ThrowsAsync<HttpException>(async () =>
            {
                await orderService.CompleteOrderAsync(1);
            });
        }

        [Test]
        public async Task CompleteOrder_MustCallSetField_ForChanginOrderStatusToFinished()
        {
            int saleQty = 5;
            int combinedQty = 10;
            int orderQty = 5;
            orderRepoMock.Setup(o => o.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => new Order()
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

            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct(1, "a", "d", 200, "a", saleQty, combinedQty, 1, 1));

            await orderService.CompleteOrderAsync(1);

            orderRepoMock.Verify(p => p.SetFieldAsync(1, "Status", OrderStatus.Finished), Times.Once);
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
