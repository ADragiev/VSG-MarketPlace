using Application.Models.GenericRepo;
using Application.Models.ImageModels.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ImageModels.Interfaces
{
    public interface IImageRepository : IGenericRepository<Image>
    {
        Task<ImageGetDto> GetImageByProductIdAsync(int productId);
    }
}
