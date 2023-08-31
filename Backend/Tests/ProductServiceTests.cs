using Application.Helpers.Constants;
using Application.Models.ExceptionModels;
using Application.Models.ImageModels.Interfaces;
using Application.Models.LentItemModels.Interfaces;
using Application.Models.OrderModels.Interfaces;
using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Intefaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> productRepoMock;
        private Mock<IMapper> mapperMock;
        private Mock<IImageService> imageServiceMock;
        private Mock<IOrderRepository> orderRepoMock;
        private IProductService productService;
        private Mock<ILentItemRepository> lentItemRepo;

        [SetUp]
        public void SetUp()
        {
            productRepoMock = new Mock<IProductRepository>();
            mapperMock = new Mock<IMapper>();
            imageServiceMock = new Mock<IImageService>();
            orderRepoMock = new Mock<IOrderRepository>();
            lentItemRepo = new Mock<ILentItemRepository>();

            productService = new ProductService(productRepoMock.Object, mapperMock.Object, imageServiceMock.Object, orderRepoMock.Object, lentItemRepo.Object);
        }

        [Test]
        public async Task GetAllForIndexAsync_MustAddBaseUrlToImage_Before_ItReturns()
        {
            var product1 = CreateProductMarketPlace();
            var product2 = CreateProductMarketPlace();
            product2.Id = 2;

            var productsDto = new List<ProductMarketPlaceGetDto>() { product1, product2 };

            productRepoMock.Setup(p => p.GetAllIndexProductsAsync()).ReturnsAsync(productsDto);

            var products = await productService.GetAllForIndexAsync();

            Assert.That(products.First().Image.StartsWith(CloudinaryConstants.baseUrl));
        }

        [Test]
        public async Task GetAllForInventoryAsync_MustAddBaseUrlToImage_Before_ItReturns()
        {
            var product1 = CreateProductInventory();
            var product2 = CreateProductInventory();
            product2.Id = 2;

            var productsDto = new List<ProductInventoryGetDto>() { product1, product2 };

            productRepoMock.Setup(p => p.GetAllInventoryProductsAsync()).ReturnsAsync(productsDto);

            var products = await productService.GetAllForInventoryAsync();

            Assert.That(products.First().Image.StartsWith(CloudinaryConstants.baseUrl));
        }


        [Test]
        public void UpdateAsync_MustThrow_IfIdIsNotValid()
        {
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            Assert.ThrowsAsync<HttpException>(() => productService.UpdateAsync(1, CreateProductUpdateDto()));
        }

        [Test]
        public void UpdateAsync_MustNotThrow_IfIdIsValid()
        {
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct());
            mapperMock.Setup(m => m.Map<Product>(It.IsAny<ProductUpdateDto>())).Returns(CreateProduct());

            Assert.DoesNotThrowAsync(() => productService.UpdateAsync(1, CreateProductUpdateDto()));
        }


        [Test]
        public void DeleteAsync_MustThrow_IfIdIsNotValid()
        {
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            Assert.ThrowsAsync<HttpException>(() => productService.DeleteAsync(1));
        }


        [Test]
        public void DeleteAsync_MustNotThrow_IfIdIsValid()
        {
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct());
            orderRepoMock.Setup(o => o.GetAllPendingOrdersAsync()).ReturnsAsync(() =>new  List<Order>());

            Assert.DoesNotThrowAsync(() => productService.DeleteAsync(1));
        }

        [Test]
        public void DeleteAsync_MustThrow_IfProductIsInPendingOrder()
        {
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => CreateProduct());
            orderRepoMock.Setup(o => o.GetAllPendingOrdersAsync()).ReturnsAsync(() => new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    Price=200,
                    OrderedBy="User",
                    ProductCode="asd",
                    Date = DateTime.Now,
                    ProductId=1,
                    ProductName="ad",
                    Status=0,
                    Qty=2,
                }
            });

            Assert.ThrowsAsync<HttpException>(() => productService.DeleteAsync(1));
        }


        private ProductMarketPlaceGetDto CreateProductMarketPlace()
        {
            return new ProductMarketPlaceGetDto()
            {
                Id = 1,
                Category = "Category",
                Name = "Name",
                Price = 200,
                SaleQty = 5,
                Image = "PublicId",
                Description = "descr"
            };
        }

        private ProductInventoryGetDto CreateProductInventory()
        {
            return new ProductInventoryGetDto()
            {
                Id = 1,
                Code = "Code",
                Category = "Category",
                Name = "Name",
                Price = 200,
                SaleQty = 5,
                CombinedQty = 10,
                Location = "Plovdiv",
                Image = "PublicId",
                Description = "descr"
            };
        }


        private static Product CreateProduct()
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

        private static ProductUpdateDto CreateProductUpdateDto()
        {
            return new ProductUpdateDto()
            {
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
    }
}
