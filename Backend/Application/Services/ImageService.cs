using Application.Helpers.Constants;
using Application.Models.Cloud;
using Application.Models.ExceptionModels;
using Application.Models.GenericModels.Dtos;
using Application.Models.GenericRepo;
using Application.Models.ImageModels.Dtos;
using Application.Models.ImageModels.Interfaces;
using Application.Models.ProductModels.Intefaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository imageRepo;
        private readonly IProductRepository productRepo;
        private readonly ICloudService cloudService;

        public ImageService(IImageRepository imageRepo,
            IProductRepository productRepo,
            ICloudService cloudService)
        {
            this.imageRepo = imageRepo;
            this.productRepo = productRepo;
            this.cloudService = cloudService;
        }

        public async Task DeleteImageByProductIdAsync(int productId)
        {
            var product = await productRepo.GetByIdAsync(productId);

            if(product == null)
            {
                throw new HttpException($"Product Id not found!", HttpStatusCode.NotFound);
            }

            var image = await imageRepo.GetImageByProductIdAsync(productId);

            if (image!=null)
            {
                await cloudService.DeleteAsync(image.PublicId);
                await imageRepo.DeleteAsync(image.Id);
            }
        }

        public async Task<GenericSimpleValueGetDto<string>> UploadImageAsync(int productId, ImageCreateDto image)
        {
            var product = await productRepo.GetByIdAsync(productId);

            if (product == null)
            {
                throw new HttpException($"Product Id not found!", HttpStatusCode.NotFound);
            }

            var publicId = await cloudService.UploadAsync(image.Image);

            await SaveImageInDatabase(productId, publicId);
            return new GenericSimpleValueGetDto<string>(CloudinaryConstants.baseUrl + publicId);
        }

        private async Task SaveImageInDatabase(int productId, string publicId)
        {
            var image = await imageRepo.GetImageByProductIdAsync(productId);
            if (image == null)
            {
                Image newImage = new Image()
                {
                    ProductId = productId,
                    PublicId = publicId
                };

                await imageRepo.CreateAsync(newImage);
            }
            else
            {
                await imageRepo.SetFieldAsync(image.Id, "PublicId", publicId);

                await cloudService.DeleteAsync(image.PublicId);
            }
        }
    }
}
