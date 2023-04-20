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
        private readonly IImageRepository imageRepo;
        private readonly ICategoryRepository categoryRepo;
        private readonly IMapper mapper;
        private readonly IImageService imageService;

        public ProductService(IProductRepository productRepo,
            IImageRepository imageRepo,
            ICategoryRepository categoryRepo,
            IMapper mapper,
            IImageService imageService)
        {
            this.productRepo = productRepo;
            this.imageRepo = imageRepo;
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
            this.imageService = imageService;
        }

        public async Task<ProductGetDto> Create(ProductCreateDto dto)
        {
            var product = mapper.Map<Product>(dto);
            var productId = await productRepo.Create(product);
            product.Id = productId;

            return mapper.Map<ProductGetDto>(product);
        }

        public async Task<List<ProductGetBaseDto>> GetAllForIndex()
        {
            var products = await productRepo.GetAllIndexProducts();
            products.ForEach(p =>
            {
                if (p.Image != null)
                {   
                    p.Image = CloudinaryConstants.baseUrl + p.Image;
                }
            });
            return products;
        }

        public async Task<ProductDetailDto> GetDetails(int id)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, productRepo);
            var product = await productRepo.GetProductDetail(id);
            if (product.Image != null)
            {
                product.Image = CloudinaryConstants.baseUrl + product.Image;
            }
            return product;
        }

        public async Task<List<ProductInventoryGetDto>> GetAllForInventory()
        {
            var products = await productRepo.GetAllInventoryProducts();
            products.ForEach(p =>
            {
                if (p.Image != null)
                {
                    p.Image = CloudinaryConstants.baseUrl + p.Image;
                }
            });
            return products;
        }

        //public async Task<ProductGetForUpdateDto> GetForUpdate(int id)
        //{
        //    await ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, productRepo);
        //    var product = await productRepo.GetForEdit(id);
        //    if (product.Image != null)
        //    {
        //        product.Image = CloudinaryConstants.baseUrl + product.Image;
        //    }
        //    return product;
        //}

        public async Task Update(int id, ProductUpdateDto dto)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, productRepo);

            var productToUpdate = mapper.Map<Product>(dto);
            productToUpdate.Id = id;

            await productRepo.Update(productToUpdate);
        }

        public async Task Delete(int id)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, productRepo);

            await imageService.DeleteImageByProductId(id);
            await productRepo.Delete(id);
        }
    }
}
