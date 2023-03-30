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

        [HttpGet]
        public List<CategoryGetDto> GetAll()
        {
            return categoryService.All();
        }


        [HttpGet("{id}")]
        public ActionResult<CategoryGetDto> GetCategoryById(int id)
        {
            try
            {
                return categoryService.GetById(id);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public CategoryGetDto CreateCategory(CategoryCreateDto dto)
        {
            return categoryService.Create(dto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CategoryUpdateDto dto)
        {
            if (id != dto.CategoryId)
            {
                return BadRequest("Ids do not match!");
            }


            try
            {
                categoryService.Update(dto);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                categoryService.Delete(id);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
