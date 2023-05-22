using Application.Models.ExceptionModels;
using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Intefaces;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class ThrowServiceTest
    {
        private List<Product> products = new List<Product>()
        {
            new Product()
            {
                Id =1,
                Code = "AAA",
                Description = "",
                Price = 200,
                Name="Acrer",
                SaleQty = 10,
                CombinedQty = 20,
                CategoryId = 1,
                LocationId = 1
            },
            new Product()
            {
                Id =2,
                Code = "BBB",
                Description = "",
                Price = 300,
                Name="Asus",
                SaleQty = 10,
                CombinedQty = 20,
                CategoryId = 1,
                LocationId = 1
            }
        };

        private readonly Mock<IProductRepository> productRepoMock = new Mock<IProductRepository>();

        [SetUp]
        public void SetUp()
        {
            productRepoMock.Setup(productRepoMock => productRepoMock.AllAsync()).ReturnsAsync(() => products);
            productRepoMock.Setup(productRepo => productRepo.GetByIdAsync(1)).ReturnsAsync(() => products.First(product => product.Id == 1));
        }

        [Test]
        public async Task ShouldThrowIfIdDoNotExist()
        {
            Assert.ThrowsAsync<HttpException>(async () => await ThrowExceptionService.ThrowExceptionWhenIdNotFound(2123, productRepoMock.Object));
        }

        [Test]
        public async Task ShouldNotThrowIfIdExists()
        {
            Assert.DoesNotThrowAsync(async () => await ThrowExceptionService.ThrowExceptionWhenIdNotFound(1, productRepoMock.Object));
        }


        [Test]
        public async Task ShouldThrowWhenOrderStatusIsNotPending()
        {
            var firstOrder = new Order()
            {
                Date = DateTime.Now,
                Id = 1,
                OrderedBy = "user",
                Price = 1,
                ProductCode = "asd",
                ProductId =1,
                ProductName = "Test",
                Status = OrderStatus.Declined,
                Qty = 1,
            };

            var secondOrder = new Order()
            {
                Date = DateTime.Now,
                Id = 1,
                OrderedBy = "user",
                Price = 1,
                ProductCode = "asd",
                ProductId = 1,
                ProductName = "Test",
                Status = OrderStatus.Finished,
                Qty = 1,
            };

            Assert.Throws<HttpException>(() => ThrowExceptionService.ThrowExceptionWhenOrderIsNotPending(firstOrder));
            Assert.Throws<HttpException>(() => ThrowExceptionService.ThrowExceptionWhenOrderIsNotPending(secondOrder));
        }

        [Test]
        public async Task ShouldNotThrowWhenOrderStatusIsPending()
        {
            var order = new Order()
            {
                Date = DateTime.Now,
                Id = 1,
                OrderedBy = "user",
                Price = 1,
                ProductCode = "asd",
                ProductId = 1,
                ProductName = "Test",
                Status = OrderStatus.Pending,
                Qty = 1,
            };

            Assert.DoesNotThrow(() => ThrowExceptionService.ThrowExceptionWhenOrderIsNotPending(order));
        }
    }
}
