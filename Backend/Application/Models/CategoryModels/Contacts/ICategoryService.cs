using Application.Models.CategoryModels.Dtos;
using Application.Models.GenericModels.Dtos;
using Application.Models.ProductModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CategoryModels.Contacts
{
    public interface ICategoryService
    {
        Task<List<CategoryGetDto>> AllAsync();
        Task<GenericSimpleValueGetDto<int>> CreateAsync(CategoryCreateUpdateDto categoryCreateDto);
        Task UpdateAsync(int id, CategoryCreateUpdateDto categoryUpdateDto);
        Task DeleteAsync(int id);
    }
}
