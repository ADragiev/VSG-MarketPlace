﻿using Application.Helpers.Attributes;
using Application.Helpers.Constants;
using Application.Helpers.Validators;
using Application.Models.GenericModels.Dtos;
using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Intefaces;
using Application.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class ProductController : BaseController
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
        [NonAdmin]
        public async Task<List<ProductMarketPlaceGetDto>> GetAllProductsForIndexPage()
        {
            return await productService.GetAllForIndexAsync();
        }

        [HttpGet("Inventory")]
        public async Task<List<ProductInventoryGetDto>> GetAllProductsForInventoryPage()
        {
            return await productService.GetAllForInventoryAsync();
        }

        [HttpPut("{id}")]
        public async Task UpdateProduct(int id, ProductUpdateDto dto)
        {
            await updateValidator.ValidateAndThrowAsync(dto);
            await productService.UpdateAsync(id, dto);
        }

        [HttpPost]
        public async Task<GenericSimpleValueGetDto<int>> CreateProduct([FromBody]ProductCreateDto dto)
        {
            await createValidator.ValidateAndThrowAsync(dto);
            return await productService.CreateAsync(dto);
        }

        [HttpDelete("{id}")]
        public async Task DeleteProduct(int id)
        {
            await productService.DeleteAsync(id);
        }
    }
}
