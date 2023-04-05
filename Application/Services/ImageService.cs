using Application.Models.GenericRepo;
using Application.Models.ImageModels.Dtos;
using Application.Models.ImageModels.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration config;
        private readonly Cloudinary cloudinary;
        private readonly IGenericRepository<Image> imageRepo;
        private readonly IGenericRepository<Product> productRepo;

        public ImageService(IConfiguration config,
            IGenericRepository<Image> imageRepo,
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

        public async Task DeleteImage(int id)
        {
            ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, imageRepo);

            var image = imageRepo.GetByID(id);
            await cloudinary.DeleteResourcesAsync(new DelResParams()
            {
                PublicIds = new List<string>() { image.ImagePublicId }
            });
            imageRepo.Delete(id);
        }

        public async Task UploadImages(int productId, ImageCreateDto image)
        {
            ThrowExceptionService.ThrowExceptionWhenIdNotFound(productId, productRepo);
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
            Image newImage = new Image()
            {
                ImageUrl = uploadResult.Url.AbsoluteUri,
                IsDefault = image.IsDefault,
                ProductCode = productId,
                ImagePublicId = uploadResult.PublicId
            };

            imageRepo.Create(newImage);
        }
    }
}
