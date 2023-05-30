using Application.Models.CacheModels.Interfaces;
using Application.Models.CategoryModels.Contacts;
using Application.Models.CategoryModels.Dtos;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private const string categoriesKey = "categories-angel";

        private readonly ICategoryRepository categoryRepo;
        private readonly IMapper mapper;
        private readonly ICacheService cacheService;

        public CategoryService(ICategoryRepository categoryRepo,
            IMapper mapper,
            ICacheService cacheService)
        {
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
            this.cacheService = cacheService;
        }

        public async Task<List<CategoryGetDto>> AllAsync()
        {
            var cachedCategories = await cacheService.GetDataAsync<List<CategoryGetDto>>(categoriesKey);

            if (cachedCategories != null)
            {
                return cachedCategories;
            }

            var categories = await categoryRepo.AllAsync();
            var categoriesDto =  mapper.Map<List<CategoryGetDto>>(categories);

            await cacheService.SetDataAsync(categoriesKey, categoriesDto, TimeSpan.FromMinutes(30));

            return categoriesDto;
        }
    }
}
