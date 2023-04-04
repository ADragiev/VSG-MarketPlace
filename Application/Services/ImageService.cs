using Application.Models.GenericRepo;
using Application.Models.ImageModels.Dtos;
using Application.Models.ImageModels.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
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
        private readonly Account account;
        private readonly IGenericRepository<Image> imageRepo;
        private readonly IGenericRepository<Product> productRepo;

        public ImageService(IGenericRepository<Image> imageRepo,
            IGenericRepository<Product> productRepo)
        {
            account = new Account(
                "dr6bomgw0",
                "186242376277331",
                "rJL4jwlbUw9RhRu8TSOQ_Gpgazs");
            this.imageRepo = imageRepo;
            this.productRepo = productRepo;
        }

        public async Task UploadImages(int productId, ImageCreateDto image)
        {
            ThrowExceptionService.ThrowExceptionWhenIdNotFound(productId, productRepo);
            Cloudinary cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;

            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                image.Image.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            string base64 = Convert.ToBase64String(bytes);


            var prefix = @"data:image/png;base64,";
            var imagePath = prefix + base64;

            var uploadParams = new ImageUploadParams()

            {
                File = new FileDescription(imagePath),
                Folder = "EndSars/img"
            };

            var uploadResult = await cloudinary.UploadAsync(@uploadParams);

            Image newImage = new Image()
            {
                ImageUrl = uploadResult.Url.AbsoluteUri,
                IsDefault = image.IsDefault,
                ProductCode = productId
            };

            imageRepo.Create(newImage);
        }
    }
}
