using Application.Models.GenericRepo;
using Application.Models.ImageModels.Dtos;
using Application.Models.ImageModels.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration config;
        private readonly Cloudinary cloudinary;
        private readonly IImageRepository imageRepo;
        private readonly IGenericRepository<Product> productRepo;

        public ImageService(IConfiguration config,
            IImageRepository imageRepo,
            IGenericRepository<Product> productRepo)
        {
            this.config = config;
            var cloudName = this.config.GetValue<string>("Cloudinary:CloudName");
            var APIKey = this.config.GetValue<string>("Cloudinary:APIKey");
            var APISecret = this.config.GetValue<string>("Cloudinary:APISecret");

            cloudinary = new Cloudinary(new Account(
                cloudName,
                APIKey,
                APISecret));

            this.imageRepo = imageRepo;
            this.productRepo = productRepo;
        }

        public async Task DeleteImageByProductId(int productId)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(productId, productRepo);

            var image = await imageRepo.GetImageByProductId(productId);

            if(image!=null)
            {
                await cloudinary.DeleteResourcesAsync(new DelResParams()
                {
                    PublicIds = new List<string>() { image.PublicId }
                });
                await imageRepo.Delete(image.Id);
            }
        }

        public async Task UploadImages(int productId, ImageCreateDto image)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(productId, productRepo);
            cloudinary.Api.Secure = true;

            //Turn file into byte array
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                image.Image.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            string base64 = Convert.ToBase64String(bytes);

            //construct image path
            var prefix = @"data:image/png;base64,";
            var imagePath = prefix + base64;

            //upload to cloudinary
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imagePath),
                Folder = "MarketPlace/Images"
            };

            var uploadResult = await cloudinary.UploadAsync(@uploadParams);

            await SaveImageInDatabase(productId, uploadResult.SecureUrl.AbsoluteUri, uploadResult.PublicId);

        }

        private async Task SaveImageInDatabase(int productId, string url, string publicId)
        {
            var image = await imageRepo.GetImageByProductId(productId);
            if(image == null)
            {
                Image newImage = new Image()
                {
                    ImageUrl = url,
                    ProductId = productId,
                    ImagePublicId = publicId
                };

                await imageRepo.Create(newImage);
            }
            else
            {
                await imageRepo.SetField(image.Id, "ImageUrl", url);
                await imageRepo.SetField(image.Id, "ImagePublicId", publicId);

                await cloudinary.DeleteResourcesAsync(new DelResParams()
                {
                    PublicIds = new List<string>() { image.PublicId }
                });
            }
        }
    }
}
