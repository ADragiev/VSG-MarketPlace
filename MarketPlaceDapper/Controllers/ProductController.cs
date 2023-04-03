﻿using Application.Models.ProductModels.Dtos;
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
        [Route("/Index")]
        public List<ProductGetBaseDto> GetAllProductsForIndexPage()
        {
            return productService.GetAllForIndex();
        }

        [HttpGet]
        [Route("/Inventory")]
        public List<ProductInventoryGetDto> GetAllProductsForInventoryPage()
        {
            return productService.GetAllForInventory();
        }

        [HttpGet]
        [Route("/Details/{id}")]
        public ProductDetailDto GetProductDetails(int id)
        {
            return productService.GetDetails(id);
        }

        [HttpPost]
        public ProductGetDto CreateProduct(ProductCreateDto dto)
        {
            return productService.Create(dto);
        }
    }
}
