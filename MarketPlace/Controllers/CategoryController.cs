using Application.Models.CategoryModels.Contacts;
using Application.Models.CategoryModels.Dtos;
using Application.Models.ExceptionModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MarketPlaceDapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
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
            return await categoryService.Create(dto);
        }

        [HttpPut("{id}")]
        public async Task UpdateCategory(int id, CategoryUpdateDto dto)
        {
            //TODO: Check if Ids are the same!
            await categoryService.Update(dto);
        }

        [HttpDelete("{id}")]
        public async Task DeleteCategory(int id)
        {
            await categoryService.Delete(id);
        }
    }
}
