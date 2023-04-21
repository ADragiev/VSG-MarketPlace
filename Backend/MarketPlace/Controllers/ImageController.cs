using Application.Models.ImageModels.Dtos;
using Application.Models.ImageModels.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;
        private readonly IValidator<ImageCreateDto> imageValidator;

        public ImageController(IImageService imageService, IValidator<ImageCreateDto> imageValidator)
        {
            this.imageService = imageService;
            this.imageValidator = imageValidator;
        }

        [HttpPost("{productId}")]
        public async Task UploadImage(int productId, [FromForm] ImageCreateDto image)
        {
            await imageValidator.ValidateAndThrowAsync(image);
            await imageService.UploadImageAsync(productId, image);
        }

        [HttpDelete("{productId}")]
        public async Task DeleteImage(int productId)
        {
            await imageService.DeleteImageByProductIdAsync(productId);
        }
    }
}
