using Application.Models.ImageModels.Dtos;
using Application.Models.ImageModels.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpPost("{productId}")]
        public async Task UploadImage(int productId, [FromForm] ImageCreateDto image)
        {
            await imageService.UploadImages(productId, image);
        }
    }
}
