using Application.Models.Cache;
using Application.Models.CategoryModels.Contacts;
using Application.Models.CategoryModels.Dtos;
using Application.Models.GenericRepo;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private const string categoriesKey = "categories";

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
            var cachedCategories = await cacheService.GetData<List<CategoryGetDto>>(categoriesKey);

            if (cachedCategories != null)
            {
                return cachedCategories;
            }

            var categories = await categoryRepo.AllAsync();
            var categoriesDto =  mapper.Map<List<CategoryGetDto>>(categories);

            await cacheService.SetData(categoriesKey, categoriesDto, DateTimeOffset.Now.AddMinutes(30));

            return categoriesDto;
        }

        public async Task<CategoryGetDto> CreateAsync(CategoryCreateDto dto)
        {
            var category = mapper.Map<Category>(dto);

            int id = await categoryRepo.CreateAsync(category);
            
            var createdCategory = await categoryRepo.GetByIdAsync(id);
            return mapper.Map<CategoryGetDto>(createdCategory);
        }

        public async Task DeleteAsync(int id)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound<Category>(id, categoryRepo);

            await categoryRepo.DeleteAsync(id);
        }

        public async Task<CategoryGetDto> GetByIdAsync(int id)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound<Category>(id, categoryRepo);
            var category = await categoryRepo.GetByIdAsync(id);

            return mapper.Map<CategoryGetDto>(category);
        }

        public async Task UpdateAsync(CategoryUpdateDto dto)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound<Category>(dto.Id, categoryRepo);

            var categoryToUpdate = mapper.Map<Category>(dto);
            await categoryRepo.UpdateAsync(categoryToUpdate);
        }
    }
}
