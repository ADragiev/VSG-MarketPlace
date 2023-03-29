using Application.Models.CategoryModels.Contacts;
using Application.Models.CategoryModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceDapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost]
        public CategoryGetDto CreateCategory(CategoryCreateDto dto)
        {
            return categoryService.Create(dto);
        }
    }
}
