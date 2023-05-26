using Application.Models.Cloud;
using Application.Models.ExceptionModels;
using Application.Models.ImageModels.Dtos;
using Application.Models.ImageModels.Interfaces;
using Application.Models.ProductModels.Intefaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class ImageServiceTests
    {
        private Mock<IImageRepository> imageRepoMock;
        private Mock<IProductRepository> productRepoMock;
        private Mock<ICloudService> cloudServiceMock;

        private IImageService imageService;

        [SetUp]
        public void SetUp()
        {
            imageRepoMock = new Mock<IImageRepository>();
            productRepoMock = new Mock<IProductRepository>();
            cloudServiceMock = new Mock<ICloudService>();

            imageService = new ImageService(imageRepoMock.Object, productRepoMock.Object, cloudServiceMock.Object);
        }

        [Test]
        public void DeleteImageByProductIdAsync_MustThrow_IfProductIdIsNotValid()
        {
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            Assert.ThrowsAsync<HttpException>(() => imageService.DeleteImageByProductIdAsync(1));
        }

        //DeleteImageByProductID
        [Test]
        public async Task DeleteImageByProductIdAsync_ShouldNotCallDeleteMethods_IfImageIsNotFound()
        {
            productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateProduct());

            imageRepoMock.Setup(i => i.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            await imageService.DeleteImageByProductIdAsync(1);

            cloudServiceMock.Verify(c => c.DeleteAsync(It.IsAny<string>()), Times.Never);
            imageRepoMock.Verify(i => i.DeleteAsync(It.IsAny<int>()), Times.Never);
        }

        //[Test]
        //public async Task DeleteImageByProductIdAsync_ShouldCallDeleteMethods_IfImageIsFound()
        //{
        //    productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateProduct());

        //    imageRepoMock.Setup(i => i.GetImageByProductIdAsync(It.IsAny<int>())).ReturnsAsync(CreateImageGetDto());

        //    await imageService.DeleteImageByProductIdAsync(1); 

        //    cloudServiceMock.Verify(c => c.DeleteAsync(It.IsAny<string>()), Times.Once);
        //    imageRepoMock.Verify(i => i.DeleteAsync(It.IsAny<int>()), Times.Once);
        //}
        //DeleteImageByProductID


        //[Test]
        //public void UploadImageAsync_MustThrow_IfProductIdIsNotValid()
        //{
        //    productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

        //    Assert.ThrowsAsync<HttpException>(() => imageService.UploadImageAsync(1, CreateImageCreateDto()));
        //}

        //[Test]
        //public async Task UploadImageAsync_ShouldCreateImageInDatabase_IfProductHasNoImage()
        //{
        //    productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateProduct());
        //    imageRepoMock.Setup(i => i.GetImageByProductIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

        //    await imageService.UploadImageAsync(1, CreateImageCreateDto());

        //    imageRepoMock.Verify(i => i.CreateAsync(It.IsAny<Image>()), Times.Once);
        //}

        //[Test]
        //public async Task UploadImageAsync_ShouldOverWriteImageInDatabaseAndDeleteOldInCloudinary_IfProductHasImage()
        //{
        //    productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateProduct());
        //    imageRepoMock.Setup(i => i.GetImageByProductIdAsync(It.IsAny<int>())).ReturnsAsync(CreateImageGetDto());
        //    cloudServiceMock.Setup(c => c.UploadAsync(It.IsAny<IFormFile>())).ReturnsAsync("PublicId");

        //    await imageService.UploadImageAsync(1, CreateImageCreateDto());

        //    imageRepoMock.Verify(i => i.SetFieldAsync(It.IsAny<int>(), "PublicId", It.IsAny<string>()), Times.Once);
        //    cloudServiceMock.Verify(c => c.DeleteAsync(It.IsAny<string>()), Times.Once);
        //}

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

        //private static ImageGetDto CreateImageGetDto()
        //{
        //    return new ImageGetDto()
        //    {
        //        Id = 1,
        //        PublicId = "PublicId"
        //    };
        //}

        //private static ImageCreateDto CreateImageCreateDto()
        //{
        //    return new ImageCreateDto()
        //    {
        //        Image = null
        //    };
        //}

        private static Image CreateImage()
        {
            return new Image()
            {
                Id= 1,
                PublicId="PublicId",
                ProductId=1
            };
        }
    }
}
