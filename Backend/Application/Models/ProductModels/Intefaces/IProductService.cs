using Application.Models.GenericRepo;
using Application.Models.ProductModels.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ProductModels.Intefaces
{
    public interface IProductService
    {
        Task<ProductGetDto> CreateAsync(ProductCreateDto dto);

        Task UpdateAsync(int id, ProductUpdateDto dto);

        Task DeleteAsync(int id); 

        Task<List<ProductMarketPlaceGetDto>> GetAllForIndexAsync();

        Task<List<ProductInventoryGetDto>> GetAllForInventoryAsync();
    }
}
