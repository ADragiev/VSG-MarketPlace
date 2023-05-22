using Application.Helpers.Constants;
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

        [HttpPost("{productId}")]
        [Authorize(Policy = IdentityConstants.AdminRolePolicyName)]
        public async Task<string> UploadImage(int productId, [FromForm] ImageCreateDto image)
        {
            await imageValidator.ValidateAndThrowAsync(image);
            return await imageService.UploadImageAsync(productId, image);
        }

        [HttpDelete("{productId}")]
        [Authorize(Policy = IdentityConstants.AdminRolePolicyName)]
        public async Task DeleteImage(int productId)
        {
            await imageService.DeleteImageByProductIdAsync(productId);
        }
    }
}
