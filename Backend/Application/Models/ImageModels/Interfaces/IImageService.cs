using Application.Models.GenericModels.Dtos;
using Application.Models.ImageModels.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ImageModels.Interfaces
{
    public interface IImageService
    {
        Task<GenericSimpleValueGetDto<string>> UploadImageAsync(int productId, ImageCreateDto images);
        Task DeleteImageByProductIdAsync(int ProductId);
    }
}
