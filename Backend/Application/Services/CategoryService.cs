using Application.Models.CategoryModels.Contacts;
using Application.Models.CategoryModels.Dtos;
using Application.Models.ExceptionModels;
using Application.Models.GenericModels.Dtos;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private const string categoriesKey = "categories-angel";

        private readonly ICategoryRepository categoryRepo;
        private readonly IMapper mapper;
        private readonly IDistributedCache cacheService;

        public CategoryService(ICategoryRepository categoryRepo,
            IMapper mapper,
            IDistributedCache cacheService)
        {
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
            this.cacheService = cacheService;
        }

        public async Task<List<CategoryGetDto>> AllAsync()
        {
            var cachedCategories = await cacheService.GetAsync(categoriesKey);

            if (cachedCategories != null)
            {
                return JsonSerializer.Deserialize<List<CategoryGetDto>>(Encoding.UTF8.GetString(cachedCategories));
            }

            var categories = await categoryRepo.AllAsync();
            var categoriesDto = mapper.Map<List<CategoryGetDto>>(categories);

            await cacheService.SetAsync(categoriesKey, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(categoriesDto)),
                                                new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) });

            return categoriesDto;
        }

        public async Task<GenericSimpleValueGetDto<int>> CreateAsync(CategoryCreateUpdateDto categoryCreateDto)
        {
            var categories = await categoryRepo.AllAsync();

            if (categories.Any(c => c.Name == categoryCreateDto.Name))
            {
                throw new HttpException("Category with that name already exists!", HttpStatusCode.BadRequest);
            }

            var category = mapper.Map<Category>(categoryCreateDto);

            var id = await categoryRepo.CreateAsync(category);
            await cacheService.RemoveAsync(categoriesKey);
            return new GenericSimpleValueGetDto<int>(id);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await categoryRepo.GetByIdAsync(id);

            if (category == null)
            {
                throw new HttpException("Category not found!", HttpStatusCode.NotFound);
            }

            await categoryRepo.DeleteAsync(id);
            await cacheService.RemoveAsync(categoriesKey);
        }

        public async Task UpdateAsync(int id, CategoryCreateUpdateDto categoryUpdateDto)
        {
            var categories = await categoryRepo.AllAsync();
            var category = categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                throw new HttpException("Category not found!", HttpStatusCode.NotFound);
            }

            if (categories.Any(c => c.Name == categoryUpdateDto.Name))
            {
                throw new HttpException("Category with that name already exists!", HttpStatusCode.BadRequest);
            }

            category = mapper.Map<Category>(categoryUpdateDto);
            category.Id = id;
            await categoryRepo.UpdateAsync(category);
            await cacheService.RemoveAsync(categoriesKey);
        }
    }
}
