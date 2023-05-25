using Application.Helpers.Constants;
using Application.Models.GenericModels.Dtos;
using Application.Models.ImageModels.Dtos;
using Application.Models.ImageModels.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class ImageController : BaseController
    {
        private readonly IImageService imageService;
        private readonly IValidator<ImageCreateDto> imageValidator;

        public ImageController(IImageService imageService, IValidator<ImageCreateDto> imageValidator)
        {
            this.imageService = imageService;
            this.imageValidator = imageValidator;
        }

        [HttpPost("{product-id}")]
        public async Task<GenericSimpleValueGetDto<string>> UploadImage(int productId, [FromForm] ImageCreateDto image)
        {
            await imageValidator.ValidateAndThrowAsync(image);
            return await imageService.UploadImageAsync(productId, image);
        }

        [HttpDelete("{product-id}")]
        public async Task DeleteImage(int productId)
        {
            await imageService.DeleteImageByProductIdAsync(productId);
        }
    }
}
