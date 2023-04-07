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
        Task<ProductGetDto> Create(ProductCreateDto dto);
        Task<ProductUpdatetDto> GetForUpdate(int id);
        Task Update(ProductUpdatetDto dto);

        Task Delete(int id); 

        Task<List<ProductGetBaseDto>> GetAllForIndex();

        Task<ProductDetailDto> GetDetails(int id);

        Task<List<ProductInventoryGetDto>> GetAllForInventory();
    }
}
