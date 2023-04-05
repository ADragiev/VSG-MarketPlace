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
            //ThrowExceptionService.ThrowExceptionWhenMoreThanOneImageIsDefault(dto.Images);
            //TODO: Да покрия и случая в който няма дефолтна или няма снимки

            var product = mapper.Map<Product>(dto);
            var productId = await productRepo.Create(product);
            product.Id = productId;

            return mapper.Map<ProductGetDto>(product);
        }

        public async Task<List<ProductGetBaseDto>> GetAllForIndex()
        {
            return await productRepo.GetAllIndexProducts();
        }

        public async Task<ProductDetailDto> GetDetails(int id)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound(id, productRepo);
            return await productRepo.GetProductDetail(id);
        }

        public async Task<List<ProductInventoryGetDto>> GetAllForInventory()
        {
            return await productRepo.GetAllInventoryProducts();
        }

    }
}
