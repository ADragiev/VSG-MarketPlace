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
        private readonly ICategoryRepository categoryRepo;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository categoryRepo, IMapper mapper)
        {
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
        }

        public async Task<List<CategoryGetDto>> AllAsync()
        {
            var categories = await categoryRepo.AllAsync();
            return mapper.Map<List<CategoryGetDto>>(categories);
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
