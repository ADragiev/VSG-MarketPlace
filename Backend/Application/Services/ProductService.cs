using Application.Helpers.Constants;
using Application.Models.CategoryModels.Contacts;
using Application.Models.GenericRepo;
using Application.Models.ImageModels.Interfaces;
using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Intefaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepo;
        private readonly IMapper mapper;
        private readonly IImageService imageService;

        public ProductService(IProductRepository productRepo,
            IMapper mapper,
            IImageService imageService)
        {
            this.mapper = mapper;
            this.imageService = imageService;
        }

        public async Task<ProductGetDto> CreateAsync(ProductCreateDto dto)
        {
            var product = mapper.Map<Product>(dto);
            var productId = await productRepo.CreateAsync(product);
            product.Id = productId;

            return mapper.Map<ProductGetDto>(product);
        }

        public async Task<List<ProductMarketPlaceGetDto>> GetAllForIndexAsync()
        {
            var products = await productRepo.GetAllIndexProductsAsync();
            products.ForEach(p =>
            {
                if (p.Image != null)
                {   
                    p.Image = CloudinaryConstants.baseUrl + p.Image;
                }
            });
            return products;
        }

        public async Task<List<ProductInventoryGetDto>> GetAllForInventoryAsync()
        {
            var products = await productRepo.GetAllInventoryProductsAsync();
            products.ForEach(p =>
            {
                if (p.Image != null)
                {
                    p.Image = CloudinaryConstants.baseUrl + p.Image;
                }
            });
            return products;
        }


        public async Task UpdateAsync(int id, ProductUpdateDto dto)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, productRepo);

            var productToUpdate = mapper.Map<Product>(dto);
            productToUpdate.Id = id;

            await productRepo.UpdateAsync(productToUpdate);
        }

        public async Task DeleteAsync(int id)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, productRepo);

            await imageService.DeleteImageByProductIdAsync(id);
            await productRepo.DeleteAsync(id);
        }
    }
}
