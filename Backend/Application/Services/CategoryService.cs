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
    }
}
