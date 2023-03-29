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
        CategoryGetDto Create(CategoryCreateDto dto);
    }
}
