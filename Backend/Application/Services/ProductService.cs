using Application.Helpers.Constants;
using Application.Models.CategoryModels.Contacts;
using Application.Models.ExceptionModels;
using Application.Models.GenericModels.Dtos;
using Application.Models.GenericRepo;
using Application.Models.ImageModels.Interfaces;
using Application.Models.LocationModels.Interfaces;
using Application.Models.OrderModels.Interfaces;
using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Intefaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System.Net;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepo;
        private readonly IMapper mapper;
        private readonly IImageService imageService;
        private readonly IOrderRepository orderRepository;

        public ProductService(IProductRepository productRepo,
            IMapper mapper,
            IImageService imageService,
            IOrderRepository orderRepository)
        {
            this.productRepo= productRepo;
            this.mapper = mapper;
            this.imageService = imageService;
            this.orderRepository = orderRepository;
        }

        public async Task<GenericSimpleValueGetDto<int>> CreateAsync(ProductCreateDto dto)
        {
            var product = mapper.Map<Product>(dto);
            var productId = await productRepo.CreateAsync(product);
            product.Id = productId;

            return new GenericSimpleValueGetDto<int>(productId);
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

            var productPendingOrdersCount = (await orderRepository.GetAllPendingOrdersAsync()).Where(o => o.ProductId == id).Count();
            if (productPendingOrdersCount > 0)
            {
                throw new HttpException("The product you want to delete has pending orders and cannot be deleted. Make sure you delete them before deleting product.", HttpStatusCode.BadRequest);
            }

            await imageService.DeleteImageByProductIdAsync(id);
            await productRepo.DeleteAsync(id);
        }
    }
}
