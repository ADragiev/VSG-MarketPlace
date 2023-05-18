using Application.Helpers.Constants;
using Application.Models.CategoryModels.Contacts;
using Application.Models.ExceptionModels;
using Application.Models.GenericRepo;
using Application.Models.ImageModels.Interfaces;
using Application.Models.LocationModels.Interfaces;
using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Intefaces;
using AutoMapper;
using Domain.Entities;
using System.Net;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepo;
        private readonly IMapper mapper;
        private readonly IImageService imageService;
        private readonly ICategoryRepository categoryRepo;
        private readonly ILocationRepository locationRepo;

        public ProductService(IProductRepository productRepo,
            IMapper mapper,
            IImageService imageService,
            ICategoryRepository categoryRepo,
            ILocationRepository locationRepo)
        {
            this.productRepo= productRepo;
            this.mapper = mapper;
            this.imageService = imageService;
            this.categoryRepo = categoryRepo;
            this.locationRepo = locationRepo;
        }

        public async Task<ProductGetDto> CreateAsync(ProductCreateDto dto)
        {
            var product = mapper.Map<Product>(dto);
            var productId = await productRepo.CreateAsync(product);
            product.Id = productId;

            var createdProduct = mapper.Map<ProductGetDto>(product);
            createdProduct.Category = (await categoryRepo.GetByIdAsync(dto.CategoryId)).Name;
            createdProduct.Location = (await locationRepo.GetByIdAsync(dto.LocationId)).Name;

            return createdProduct;
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


        public async Task<ProductGetDto> UpdateAsync(int id, ProductUpdateDto dto)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, productRepo);

            var productToUpdate = mapper.Map<Product>(dto);
            productToUpdate.Id = id;
            await productRepo.UpdateAsync(productToUpdate);

            var updatedProduct = mapper.Map<ProductGetDto>(productToUpdate);
            updatedProduct.Category = (await categoryRepo.GetByIdAsync(dto.CategoryId)).Name;
            updatedProduct.Location = (await locationRepo.GetByIdAsync(dto.LocationId)).Name;

            return updatedProduct;
        }

        public async Task DeleteAsync(int id)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, productRepo);

            var productPendingOrdersCount = await productRepo.GetProductPendingOrdersCountAsync(id);

            if(productPendingOrdersCount > 0)
            {
                throw new HttpException("The product you want to delete has pending orders and cannot be deleted. Make sure you delete them before deleting product.", HttpStatusCode.BadRequest);
            }

            await imageService.DeleteImageByProductIdAsync(id);
            await productRepo.DeleteAsync(id);
        }
    }
}
