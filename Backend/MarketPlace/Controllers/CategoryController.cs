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
        }

        [HttpGet]
        public async Task<List<CategoryGetDto>> GetAll()
        {
            return await categoryService.All();
        }


        [HttpGet("{id}")]
        public async Task<CategoryGetDto> GetCategoryById(int id)
        {
            return await categoryService.GetById(id);
        }

        [HttpPost]
        public async Task<CategoryGetDto> CreateCategory(CategoryCreateDto dto)
        {
            ValidationResult result = await createValidator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            return await categoryService.Create(dto);
        }

        [HttpPut("{id}")]
        public async Task UpdateCategory(int id, CategoryUpdateDto dto)
        {
            await categoryService.Update(dto);
        }

        [HttpDelete("{id}")]
        public async Task DeleteCategory(int id)
        {
            await categoryService.Delete(id);
        }
    }
}
