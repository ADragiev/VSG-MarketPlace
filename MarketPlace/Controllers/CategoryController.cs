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
        public List<CategoryGetDto> GetAll()
        {
            return categoryService.All();
        }


        [HttpGet("{id}")]
        public CategoryGetDto GetCategoryById(int id)
        {
            return categoryService.GetById(id);
        }

        [HttpPost]
        public CategoryGetDto CreateCategory(CategoryCreateDto dto)
        {
            return categoryService.Create(dto);
        }

        [HttpPut("{id}")]
        public void UpdateCategory(int id, CategoryUpdateDto dto)
        {
            //TODO: Check if Ids are the same!
            categoryService.Update(dto);
        }

        [HttpDelete("{id}")]
        public void DeleteCategory(int id)
        {
            categoryService.Delete(id);
        }
    }
}
