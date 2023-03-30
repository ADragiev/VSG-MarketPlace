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
        List<CategoryGetDto> All();

        CategoryGetDto GetById(int id);

        CategoryGetDto Create(CategoryCreateDto dto);

        void Delete(int id);

        void Update(CategoryUpdateDto dto);
    }
}
