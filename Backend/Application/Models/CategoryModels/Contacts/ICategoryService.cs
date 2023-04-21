using Application.Models.CategoryModels.Dtos;
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

        Task<CategoryGetDto> GetByIdAsync(int id);

        Task<CategoryGetDto> CreateAsync(CategoryCreateDto dto);

        Task DeleteAsync(int id);

        Task UpdateAsync(CategoryUpdateDto dto);
    }
}
