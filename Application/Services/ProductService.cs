using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Intefaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<Image> imageRepo;
        private readonly IGenericRepository<Category> categoryRepo;
        private readonly IMapper mapper;

        public ProductService(IGenericRepository<Product> productRepo,
            IGenericRepository<Image> imageRepo,
            IGenericRepository<Category> categoryRepo,
            IMapper mapper)
        {
            this.productRepo = productRepo;
            this.imageRepo = imageRepo;
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
        }

        public ProductGetDto Create(ProductCreateDto dto)
        {
            ThrowExceptionService.ThrowExceptionWhenSaleQtyBiggerThanCombinedQty(dto.SaleQty, dto.CombinedQty);
            ThrowExceptionService.ThrowExceptionWhenIdNotFound<Category>(dto.CategoryId, categoryRepo);
            ThrowExceptionService.ThrowExceptionWhenMoreThanOneImageIsDefault(dto.Images);
            //TODO: Да покрия и случая в който няма дефолтна или няма снимки

            var product = mapper.Map<Product>(dto);
            var productId = productRepo.Create(product);

            var images = mapper.Map<List<Image>>(dto.Images);

            foreach (var image in images)
            {
                image.ProductCode = productId;
                imageRepo.Create(image);
            }

            var createdProduct = productRepo.GetByID(productId);
            return mapper.Map<ProductGetDto>(createdProduct);

        }
    }
}
