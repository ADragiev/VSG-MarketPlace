using Application.Models.CategoryModels.Contacts;
using Application.Models.CategoryModels.Dtos;
using Application.Models.GenericModels.Dtos;
using Application.Models.ProductModels.Dtos;
using Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly IValidator<CategoryCreateUpdateDto> validator;

        public CategoryController(ICategoryService categoryService,
            IValidator<CategoryCreateUpdateDto> validator)
        {
            this.categoryService = categoryService;
            this.validator = validator;
        }

        [HttpGet]
        public async Task<List<CategoryGetDto>> GetAll()
        {
            return await categoryService.AllAsync();
        }

        [HttpPut("{id}")]
        public async Task UpdateCategory(int id, CategoryCreateUpdateDto categoryUpdateDto)
        {
            await validator.ValidateAndThrowAsync(categoryUpdateDto);
            await categoryService.UpdateAsync(id, categoryUpdateDto);
        }

        [HttpPost]
        public async Task<GenericSimpleValueGetDto<int>> CreateCategory([FromBody] CategoryCreateUpdateDto categoryCreateDto)
        {
            await validator.ValidateAndThrowAsync(categoryCreateDto);
            return await categoryService.CreateAsync(categoryCreateDto);
        }

        [HttpDelete("{id}")]
        public async Task DeleteCategory(int id)
        {
            await categoryService.DeleteAsync(id);
        }
    }
}
