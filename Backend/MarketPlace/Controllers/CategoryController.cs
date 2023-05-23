using Application.Models.CategoryModels.Contacts;
using Application.Models.CategoryModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<List<CategoryGetDto>> GetAll()
        {
            return await categoryService.AllAsync();
        }
    }
}
