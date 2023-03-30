using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceDapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return NoContent();
        }

        [HttpPost]
        public ActionResult<ProductGetDto> CreateProduct(ProductCreateDto dto)
        {
            try
            {
                return Ok(productService.Create(dto));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
