using Application.Helpers.Constants;
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
        private readonly IGenericRepository<Image> imageRepo;
        private readonly IGenericRepository<Category> categoryRepo;
        private readonly IMapper mapper;
        private readonly IImageService imageService;

        public ProductService(IProductRepository productRepo,
            IGenericRepository<Image> imageRepo,
            IGenericRepository<Category> categoryRepo,
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
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound<Category>(dto.CategoryId, categoryRepo);

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
            return await productRepo.GetAllInventoryProducts();
        }

        public async Task<ProductGetForUpdateDto> GetForUpdate(int id)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, productRepo);
            var product = await productRepo.GetForEdit(id);
            if (product.Image != null)
            {
                product.Image = CloudinaryConstants.baseUrl + product.Image;
            }
            return product;
        }

        public async Task Update(ProductUpdateDto dto)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(dto.Id, productRepo);
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound<Category>(dto.CategoryId, categoryRepo);

            var productToUpdate = mapper.Map<Product>(dto);

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
