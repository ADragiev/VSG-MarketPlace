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
        Task<List<CategoryGetDto>> All();

        Task<CategoryGetDto> GetById(int id);

        Task<CategoryGetDto> Create(CategoryCreateDto dto);

        Task Delete(int id);

        Task Update(CategoryUpdateDto dto);
    }
}
