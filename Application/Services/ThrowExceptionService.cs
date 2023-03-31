using Application.Models.ExceptionModels;
using Application.Models.ImageModels.Dtos;
using Domain.Entities;
using Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public static class ThrowExceptionService
    {
        public static void ThrowExceptionWhenIdNotFound<T>(int id, IGenericRepository<T> repo)
        {
            var entity = repo.GetByID(id);

            if (entity == null)
            {
                throw new HttpException($"{typeof(T).Name} Id not found!", HttpStatusCode.NotFound);
            }
        }

        public static void ThrowExceptionWhenSaleQtyBiggerThanCombinedQty(int saleQty, int combinedQty)
        {
            if (saleQty > combinedQty)
            {
                throw new HttpException("Sale qty cannot be bigger than combined qty", HttpStatusCode.BadRequest);
            }
        }

        public static void ThrowExceptionWhenMoreThanOneImageIsDefault(List<ImageCreateDto> images)
        {
            var defaultImagesCount = images.Count(i => i.IsDefault == true);

            if (defaultImagesCount > 1)
            {
                throw new HttpException("There can be only one default image", HttpStatusCode.BadRequest);
            }
        }
    }
}
