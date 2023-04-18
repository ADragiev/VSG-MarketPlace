using Application.Helpers.Validators;
using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Intefaces;
using Application.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IValidator<ProductUpdateDto> updateValidator;
        private readonly IValidator<ProductCreateDto> createValidator;

        public ProductController(IProductService productService,
            IValidator<ProductUpdateDto> updateValidator,
            IValidator<ProductCreateDto> createValidator)
        {
            this.productService = productService;
            this.updateValidator = updateValidator;
            this.createValidator = createValidator;
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

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<ProductGetForUpdateDto> GetProductForEdit(int id)
        {
            return await productService.GetForUpdate(id);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task UpdateProduct(int id, ProductUpdateDto dto)
        {
            ValidationResult result = await updateValidator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            await productService.Update(id, dto);
        }

        [HttpPost]
        public async Task<ProductGetDto> CreateProduct([FromBody]ProductCreateDto dto)
        {
            ValidationResult result = await createValidator.ValidateAsync(dto);
            if(!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            return await productService.Create(dto);
        }

        [HttpDelete("{id}")]
        public async Task DeleteProduct(int id)
        {
            await productService.Delete(id);
        }
    }
}
