using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceDapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<List<ProductGetBaseDto>> GetAllProductsForIndexPage()
        {
            return await productService.GetAllForIndex();
        }

        [HttpGet]
        [Route("Inventory")]
        public async Task<List<ProductInventoryGetDto>> GetAllProductsForInventoryPage()
        {
            return await productService.GetAllForInventory();
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<ProductDetailDto> GetProductDetails(int id)
        {
            return await productService.GetDetails(id);
        }

        [HttpPost]
        public async Task<ProductGetDto> CreateProduct(ProductCreateDto dto)
        {
            return await productService.Create(dto);
        }
    }
}
