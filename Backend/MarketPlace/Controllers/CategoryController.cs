using Application.Helpers.Validators;
using Application.Models.CategoryModels.Contacts;
using Application.Models.CategoryModels.Dtos;
using Application.Models.ExceptionModels;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IValidator<CategoryCreateDto> createValidator;

        public CategoryController(ICategoryService categoryService,
            IValidator<CategoryCreateDto> createValidator)
        {
            this.createValidator = createValidator;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<List<CategoryGetDto>> GetAll()
        {
            return await categoryService.AllAsync();
        }


        //[HttpGet("{id}")]
        //public async Task<CategoryGetDto> GetCategoryById(int id)
        //{
        //    return await categoryService.GetByIdAsync(id);
        //}

        //[HttpPost]
        //public async Task<CategoryGetDto> CreateCategory(CategoryCreateDto dto)
        //{
        //    await createValidator.ValidateAndThrowAsync(dto);
        //    return await categoryService.CreateAsync(dto);
        //}

        //[HttpPut("{id}")]
        //public async Task UpdateCategory(int id, CategoryUpdateDto dto)
        //{
        //    await categoryService.UpdateAsync(dto);
        //}

        //[HttpDelete("{id}")]
        //public async Task DeleteCategory(int id)
        //{
        //    await categoryService.DeleteAsync(id);
        //}
    }
}
