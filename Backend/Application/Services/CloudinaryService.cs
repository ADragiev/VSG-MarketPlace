using Application.Models.Cloud;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CloudinaryService : ICloudService
    {
        private readonly Cloudinary cloudinary;
        public CloudinaryService(IConfiguration config)
        {
            var cloudName = config.GetValue<string>("Cloudinary:CloudName");
            var APIKey = config.GetValue<string>("Cloudinary:APIKey");
            var APISecret = config.GetValue<string>("Cloudinary:APISecret");

            cloudinary = new Cloudinary(new Account(
                cloudName,
                APIKey,
                APISecret));
        }

        public async Task DeleteAsync(string publicId)
        {
            await cloudinary.DeleteResourcesAsync(new DelResParams()
            {
                PublicIds = new List<string>() { publicId }
            });
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            cloudinary.Api.Secure = true;

            //Turn file into byte array
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
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
                Folder = "MarketPlace/"
            };

            var uploadResult = await cloudinary.UploadAsync(@uploadParams);
            return uploadResult.PublicId;
        }
    }
}
